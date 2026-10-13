using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasSchemeConfiguration : IEntityTypeConfiguration<FasScheme>
{
    public void Configure(EntityTypeBuilder<FasScheme> builder)
    {
        builder.ToTable("FasSchemes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.SchemeCode).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.SchemeName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.SchemeType).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.HasIndex(x => x.SchemeCode).IsUnique();
    }
}
