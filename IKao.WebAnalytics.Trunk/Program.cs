using System.Text.Json;
using AutoMapper;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.Mapping;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Infrastructure;
using IKao.WebAnalytics.Infrastructure.Options;
using IKao.WebAnalytics.Trunk;
using IKao.WebAnalytics.Trunk.Application.Consumers;
using MassTransit;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionOptions = configuration
            .GetSection("ConnectionStrings")
            .Get<ConnectionStrings>();

        if (connectionOptions == null)
            throw new NullReferenceException(nameof(ConnectionStrings));
        
        var postgresOptionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        var postgresOptions = postgresOptionsBuilder.UseNpgsql(connectionOptions.RelationDatabase, x =>
        {
            x.UseNodaTime();
        }).Options;

        services.AddScoped<PostgresDbContext>(db => new PostgresDbContext(postgresOptions));
        
        var mapperConfiguration = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new DTOMappingProfile());
        });
        
        var mapper = mapperConfiguration.CreateMapper();
        services.AddSingleton(mapper);
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ShortGamesUpdateConsumer>();
            x.AddConsumer<LongGamesUpdateConsumer>();
            
            x.UsingRabbitMq((busContext, cfg) =>
            {
                cfg.ReceiveEndpoint("trunk.short", e =>
                {
                    e.Durable = true;
                    e.Bind<IShortGamesUpdateRequestMessage>();
                    e.ConfigureConsumer<ShortGamesUpdateConsumer>(busContext);
                    e.ConcurrentMessageLimit = 1;
                    e.UseConcurrencyLimit(1);
                    e.PrefetchCount = 1;
                });
                
                cfg.ReceiveEndpoint("trunk.long", e =>
                {
                    e.Durable = true;
                    e.Bind<ILongGamesUpdateRequestMessage>();
                    e.ConfigureConsumer<LongGamesUpdateConsumer>(busContext);
                    e.ConcurrentMessageLimit = 1;
                    e.UseConcurrencyLimit(1);
                    e.PrefetchCount = 1;
                });
                
                cfg.ConfigureEndpoints(busContext);
            });
        });
        
        services.AddScoped<IEFRepository, BaseEFRepository>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();