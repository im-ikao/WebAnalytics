using IKao.WebAnalytics;
using IKao.WebAnalytics.Infrastructure;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        
        services.AddScoped<PostgresDbContext>(db => new PostgresDbContext(new DbContextOptions<PostgresDbContext>()
        {
        }));
    })
    .Build();

host.Run();