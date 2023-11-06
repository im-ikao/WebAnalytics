using IKao.WebAnalytics.Domain.Model;
using IKao.WebAnalytics.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKao.WebAnalytics.Infrastructure.Configurations;

public class DeveloperConfiguration : IEntityTypeConfiguration<Developer> 
{
    public void Configure(EntityTypeBuilder<Developer> builder)
    {
        builder.ToTable("developers");
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