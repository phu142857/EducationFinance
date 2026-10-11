using EducationFinance.Domain.Modules.Finance.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class FeeConfigurationConfiguration
    : IEntityTypeConfiguration<FeeConfiguration>
{
    public void Configure(EntityTypeBuilder<FeeConfiguration> builder)
    {
        builder.ToTable("FeeConfigurations");

        builder.HasKey(x => x.ConfigurationId);

        builder.Property(x => x.ConfigurationId)
            .HasColumnName("configuration_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id");

        builder.Property(x => x.CohortId)
            .HasColumnName("cohort_id");

        builder.Property(x => x.FeeType)
            .HasColumnName("fee_type")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.ComponentName)
            .HasColumnName("component_name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.AcademicTerm)
            .HasColumnName("academic_term")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Cohort>()
            .WithMany()
            .HasForeignKey(x => x.CohortId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
