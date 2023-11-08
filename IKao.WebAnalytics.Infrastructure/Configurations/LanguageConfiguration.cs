using IKao.WebAnalytics.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKao.WebAnalytics.Infrastructure.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.ToTable("languages");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedNever();

        builder
            .Property(p => p.Value)
            .HasColumnName("name")
            .HasColumnType("nvarchar(128)")
            .IsRequired();
    }
}