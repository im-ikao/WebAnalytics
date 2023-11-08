using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Infrastructure.Configurations;
using IKao.WebAnalytics.Infrastructure.Configurations.Relation;
using Microsoft.EntityFrameworkCore;

namespace IKao.WebAnalytics.Infrastructure;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
                
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    { 
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new GameConfiguration());
        builder.ApplyConfiguration(new DeveloperConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new LanguageConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new GameTagRelationConfiguration());
        builder.ApplyConfiguration(new GameCategoryRelationConfiguration());
        builder.ApplyConfiguration(new GameLanguageRelationConfiguration());
    }
    
    public DbSet<Game> Games { get; set; }
    public DbSet<Developer> Developers { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<GameCategoryRelation> CategoryRelations { get; set; }
    public DbSet<GameLanguageRelation> LanguageRelations { get; set; }
    public DbSet<GameTagRelation> TagRelations { get; set; }
}