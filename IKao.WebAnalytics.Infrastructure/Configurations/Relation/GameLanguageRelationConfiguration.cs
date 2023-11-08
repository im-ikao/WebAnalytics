using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IKao.WebAnalytics.Infrastructure.Configurations.Relation;

public class GameLanguageRelationConfiguration : IEntityTypeConfiguration<GameLanguageRelation>
{
    public void Configure(EntityTypeBuilder<GameLanguageRelation> builder)
    {
        builder.ToTable("game_languages");

        var appIdConverter = new ValueConverter<AppId, int>(
            from => from.Value,
            to => new AppId(to));
        
        builder.Property(p => p.GameId)
            .HasConversion(appIdConverter)
            .HasColumnName("game_id")
            .HasColumnType("int")
            .ValueGeneratedNever();

        builder
            .Property(p => p.LanguageId)
            .HasColumnName("language_id")
            .HasColumnType("int")
            .IsRequired();
    }
}