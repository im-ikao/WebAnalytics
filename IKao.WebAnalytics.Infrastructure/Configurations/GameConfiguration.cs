using AutoMapper.Execution;
using IKao.WebAnalytics.Domain;
using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IKao.WebAnalytics.Infrastructure.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game> {

    public void Configure(EntityTypeBuilder<Game> builder) {
        builder.ToTable("games");

        var appIdConverter = new ValueConverter<AppId, int>(
            from => from.Value, 
            to => new AppId(to));
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(appIdConverter)
            .ValueGeneratedNever();
        
        var titleConverter = new ValueConverter<Name, string>(
            from => from.Value, 
            to => new Name(to));
        
        var descriptionConverter = new ValueConverter<Description, string>(
            from => from.Value, 
            to => new Description(to));
        
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
        
        var developerConverter = new ValueConverter<Developer, int>(
            from => from.Id, 
            to => new Developer(to));
        
        builder
            .Property(p => p.Developer)
            .HasConversion(developerConverter)
            .HasColumnName("developer_id")
            .HasColumnType("int")
            .IsRequired();
        
        var ageConverter = new ValueConverter<AgeRating, int>(
            v => (int) v,
            v => (AgeRating) v);
        
        builder
            .Property(e => e.Age)
            .HasConversion(ageConverter)
            .HasColumnName("age_rating")
            .HasColumnType("int")
            .IsRequired();
        
        builder.OwnsOne(b => b.Rating, pn => {
                pn.Property(x => x.Value).HasColumnName("rating_value");
                pn.Property(x => x.Value).HasColumnType("int");
                pn.Property(x => x.Value).IsRequired();

                pn.Property(x => x.Count).HasColumnName("rating_count");
                pn.Property(x => x.Count).HasColumnType("int");
                pn.Property(x => x.Count).IsRequired();
        });
        
        builder.OwnsOne(b => b.Media, pn => {
            pn.Property(x => x.Cover).HasColumnName("media_cover");
            pn.Property(x => x.Cover).HasColumnType("string");
            pn.Property(x => x.Cover).IsRequired();

            pn.Property(x => x.Icon).HasColumnName("media_icon");
            pn.Property(x => x.Icon).HasColumnType("string");
            pn.Property(x => x.Icon).IsRequired();
        });
        
        var playConverter = new ValueConverter<Url, string>(
            from => from.Value, 
            to => new Url(to));
        
        builder
            .Property(p => p.Play)
            .HasConversion(playConverter)
            .HasColumnName("play_link")
            .HasColumnType("string")
            .IsRequired();
        
    }
}