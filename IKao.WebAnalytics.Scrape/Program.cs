using System.Text.Json;
using AutoMapper;
using IKao.WebAnalytics.Domain.Abstraction;
using IKao.WebAnalytics.Domain.Mapping;
using IKao.WebAnalytics.Domain.Message;
using IKao.WebAnalytics.Infrastructure;
using IKao.WebAnalytics.Infrastructure.Options;
using IKao.WebAnalytics.RateLimit;
using IKao.WebAnalytics.Scrape;
using IKao.WebAnalytics.Scrape.Domain.Response;
using IKao.WebAnalytics.Scrape.Infrastructure;
using IKao.WebAnalytics.Scrape.Infrastructure.Mapping;
using IKao.WebAnalytics.Scrape.Infrastructure.Normalizer;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using RestSharp.Serializers.Json;

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
            mc.AddProfile(new RequestMappingProfile());
        });
        
        var mapper = mapperConfiguration.CreateMapper();
        services.AddSingleton(mapper);
        
        // TODO: TO REST CLIENT PROVIDER
        var clientConfiguration = new RestClientOptions()
        {
            BaseUrl = new Uri("https://yandex.ru/games/api/"),
            ThrowOnAnyError = false,
            MaxTimeout = 10000,
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36"
        }; // TODO: ENV
        
        var client = new RestClient(clientConfiguration, configureSerialization: 
            config => config.UseSystemTextJson(new JsonSerializerOptions()
            {
                IncludeFields = true
            }), useClientFactory: true);
        
        client.AddDefaultHeader("Content-Type", "application/json");
        client.AddDefaultHeader("Accept", "application/json");

        services.AddSingleton(client);
        // END REST CLIENT
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((busContext, cfg) =>
            {
                cfg.Publish<IShortGamesUpdateRequestMessage>(mqTopology =>
                {
                    mqTopology.Durable = true;
                });
                
                cfg.Publish<ILongGamesUpdateRequestMessage>(mqTopology =>
                {
                    mqTopology.Durable = true;
                });
                
                cfg.ConfigureEndpoints(busContext);
            });
        });
        
        var rateLimit = TimeLimiter.GetFromMaxCountByInterval(1, TimeSpan.FromSeconds(3));
        services.AddSingleton(rateLimit);
        
        services.AddSingleton<IResponseNormalizer<GetShortGamesResponse>, GetShortGamesResponsesNormalizer<GetShortGamesResponse>>();
        services.AddSingleton<IResponseNormalizer<GetLongGamesResponse>, GetShortGamesResponsesNormalizer<GetLongGamesResponse>>();
        
        services.AddScoped<IEFRepository, BaseEFRepository>();
        services.AddScoped<BaseLongGameScrape>();
        services.AddScoped<BaseShortGamesScrape>();
        
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();