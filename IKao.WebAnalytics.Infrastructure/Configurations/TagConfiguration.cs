using IKao.WebAnalytics.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKao.WebAnalytics.Infrastructure.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedNever();

        builder
            .Property(p => p.Name)
            .HasColumnName("name")
            .HasColumnType("nvarchar(128)")
            .IsRequired();
    }
}