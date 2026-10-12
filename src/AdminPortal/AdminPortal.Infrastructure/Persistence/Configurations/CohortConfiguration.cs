using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class CohortConfiguration : IEntityTypeConfiguration<Cohort>
{
    public void Configure(EntityTypeBuilder<Cohort> builder)
    {
        builder.ToTable("Cohorts");

        builder.HasKey(x => x.CohortId);

        builder.Property(x => x.CohortId)
            .HasColumnName("cohort_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id")
            .IsRequired();

        builder.Property(x => x.CohortYear)
            .HasColumnName("cohort_year")
            .IsRequired();

        builder.Property(x => x.LevelName)
            .HasColumnName("level_name")
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
