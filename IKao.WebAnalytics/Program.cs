using IKao.WebAnalytics;
using IKao.WebAnalytics.Infrastructure;
using IKao.WebAnalytics.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
        var configuration = context.Configuration;
        
        var connectionOptions = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

        if (connectionOptions == null)
            throw new NullReferenceException(nameof(ConnectionStrings));
        
        services.AddSingleton(connectionOptions);
        
        var postgresOptionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
        var postgresOptions = postgresOptionsBuilder.UseNpgsql(connectionOptions.RelationDatabase, x =>
        {
            x.UseNodaTime();
        }).Options;

        services.AddScoped<PostgresDbContext>(db => new PostgresDbContext(postgresOptions));
    })
    .Build();

host.Run();