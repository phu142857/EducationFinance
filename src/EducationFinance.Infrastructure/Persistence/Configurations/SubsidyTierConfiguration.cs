using EducationFinance.Domain.Modules.FAS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class SubsidyTierConfiguration
    : IEntityTypeConfiguration<SubsidyTier>
{
    public void Configure(EntityTypeBuilder<SubsidyTier> builder)
    {
        builder.ToTable("SubsidyTiers");

        builder.HasKey(x => x.TierId);

        builder.Property(x => x.TierId)
            .HasColumnName("tier_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchemeId)
            .HasColumnName("scheme_id")
            .IsRequired();

        builder.Property(x => x.TierName)
            .HasColumnName("tier_name")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.FeeComponentName)
            .HasColumnName("fee_component_name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.SubsidyPercentage)
            .HasColumnName("subsidy_percentage")
            .HasPrecision(5, 2);

        builder.Property(x => x.FixedSubsidyAmount)
            .HasColumnName("fixed_subsidy_amount")
            .HasPrecision(18, 2);

        builder.HasOne<FasScheme>()
            .WithMany()
            .HasForeignKey(x => x.SchemeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
