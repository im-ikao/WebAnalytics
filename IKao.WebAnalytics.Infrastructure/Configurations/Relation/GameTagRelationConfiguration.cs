using IKao.WebAnalytics.Domain.Model.Relation;
using IKao.WebAnalytics.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IKao.WebAnalytics.Infrastructure.Configurations.Relation;

public class GameTagRelationConfiguration : IEntityTypeConfiguration<GameTagRelation>
{
    public void Configure(EntityTypeBuilder<GameTagRelation> builder)
    {
        builder.ToTable("game_tags");

        var appIdConverter = new ValueConverter<AppId, int>(
            from => from.Value,
            to => new AppId(to));
        
        builder.Property(p => p.GameId)
            .HasConversion(appIdConverter)
            .HasColumnName("game_id")
            .HasColumnType("int")
            .ValueGeneratedNever();

        builder
            .Property(p => p.TagId)
            .HasColumnName("tag_id")
            .HasColumnType("int")
            .IsRequired();
    }
}