using AutoMapper.Execution;
using IKao.WebAnalytics.Domain;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IKao.WebAnalytics.Infrastructure.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game> 
{
    public void Configure(EntityTypeBuilder<Game> builder) {
        
        builder.ToTable("games");

        var appIdConverter = new ValueConverter<AppId, int>(
            from => from.Value, 
            to => new AppId(to));
        
        var titleConverter = new ValueConverter<Name, string>(
            from => from.Value, 
            to => new Name(to));
        
        var descriptionConverter = new ValueConverter<Description, string>(
            from => from.Value, 
            to => new Description(to));
        
        var developerConverter = new ValueConverter<Developer, int>(
            from => from.Id, 
            to => new Developer(to));
        
        var ageConverter = new ValueConverter<AgeRating, int>(
            v => (int) v,
            v => (AgeRating) v);
        
        var counterConverter = new ValueConverter<Counter, int>(
            from => from.Value, 
            to => new Counter(to));
        
        var urlConverter = new ValueConverter<Url, string>(
            from => from.Value, 
            to => new Url(to));
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(appIdConverter)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedNever();
        
        builder
            .Property(p => p.Title)
            .HasConversion(titleConverter)
            .HasColumnName("title")
            .HasColumnType("nvarchar(128)")
            .IsRequired();
        
        builder
            .Property(p => p.Description)
            .HasConversion(descriptionConverter)
            .HasColumnName("description")
            .HasColumnType("nvarchar(255)")
            .IsRequired();
        
        builder
            .Property(p => p.Seo)
            .HasConversion(descriptionConverter)
            .HasColumnName("seo_description")
            .HasColumnType("nvarchar(255)")
            .IsRequired();
        
        builder
            .Property(p => p.Instruction)
            .HasConversion(descriptionConverter)
            .HasColumnName("instruction_description")
            .HasColumnType("nvarchar(255)")
            .IsRequired();
        
        builder
            .Property(e => e.Age)
            .HasConversion(ageConverter)
            .HasColumnName("age_rating")
            .HasColumnType("int")
            .IsRequired();
        
        builder
            .Property(p => p.Players)
            .HasConversion(counterConverter)
            .HasColumnName("counter_players")
            .HasColumnType("int")
            .IsRequired();
        
        builder
            .Property(p => p.Play)
            .HasConversion(urlConverter)
            .HasColumnName("play_link")
            .HasColumnType("nvarchar(255)")
            .IsRequired();
        
        builder
            .Property(x => x.Publish)
            .HasColumnName("publish_date")
            .HasColumnType("timestamp with time zone");
        
        builder
            .Property(x => x.CreationDate)
            .HasColumnName("creation_date")
            .HasColumnType("timestamp with time zone");
        
        builder
            .Property(x => x.ModificationDate)
            .HasColumnName("modification_date")
            .HasColumnType("timestamp with time zone");
        
        builder
            .Property(x => x.DeletionDate)
            .HasColumnName("deletion_date")
            .HasColumnType("timestamp with time zone");
        
        builder
            .Property(x => x.IsDeleted)
            .HasColumnName("published_date")
            .HasColumnType("boolean")
            .IsRequired();
        
        builder.OwnsOne(b => b.Rating, pn => {
            pn.Property(x => x.Value).HasColumnName("rating_value");
            pn.Property(x => x.Value).HasColumnType("int");
            pn.Property(x => x.Value).IsRequired();

            pn.Property(x => x.Count).HasColumnName("rating_count");
            pn.Property(x => x.Count).HasColumnType("int");
            pn.Property(x => x.Count).IsRequired();
        });
        
        builder.OwnsOne(b => b.Media, pn =>
        {
            pn.OwnsOne(b => b.Cover, pn =>
            {
                pn.Property(x => x.Value).HasColumnName("media_cover");
                pn.Property(x => x.Value).HasColumnType("nvarchar(255)");
                pn.Property(x => x.Value).IsRequired();
            });
            
            pn.OwnsOne(b => b.Icon, pn =>
            {
                pn.Property(x => x.Value).HasColumnName("media_icon");
                pn.Property(x => x.Value).HasColumnType("nvarchar(255)");
                pn.Property(x => x.Value).IsRequired();
            });
        });
        
        builder
            .HasOne(s => s.Developer)
            .WithMany(p => p.RelationGames)
            .HasForeignKey("developer_id")
            .IsRequired();
    }
}