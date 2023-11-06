using IKao.WebAnalytics.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace IKao.WebAnalytics.Infrastructure;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
                
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Game> Games { get; set; }
}