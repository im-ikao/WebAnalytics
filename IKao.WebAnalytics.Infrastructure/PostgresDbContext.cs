﻿using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IKao.WebAnalytics.Infrastructure;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
                
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    { 
        builder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=ygscraper;Username=ygscraper;Password=ygscraper;Include Error Detail=true",
            o =>
            {
                o.UseNodaTime();
            });
        
        base.OnConfiguring(builder);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new GameConfiguration());
        builder.ApplyConfiguration(new DeveloperConfiguration());
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