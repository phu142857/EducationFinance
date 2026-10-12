using AdminPortal.Domain.Modules.FAS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasSchemeConfiguration
    : IEntityTypeConfiguration<FasScheme>
{
    public void Configure(EntityTypeBuilder<FasScheme> builder)
    {
        builder.ToTable("FASSchemes");

        builder.HasKey(x => x.SchemeId);

        builder.Property(x => x.SchemeId)
            .HasColumnName("scheme_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchemeName)
            .HasColumnName("scheme_name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.IncomeCeiling)
            .HasColumnName("income_ceiling")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.RulesJson)
            .HasColumnName("rules_json")
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();
    }
}
