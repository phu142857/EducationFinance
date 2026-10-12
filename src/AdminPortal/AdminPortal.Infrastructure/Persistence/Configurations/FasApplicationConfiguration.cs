using AdminPortal.Domain.Modules.FAS.Entities;
using AdminPortal.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasApplicationConfiguration
    : IEntityTypeConfiguration<FasApplication>
{
    public void Configure(EntityTypeBuilder<FasApplication> builder)
    {
        builder.ToTable("FASApplications");

        builder.HasKey(x => x.ApplicationId);

        builder.Property(x => x.ApplicationId)
            .HasColumnName("application_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.ExternalParentId)
            .HasColumnName("external_parent_id")
            .IsRequired();

        builder.Property(x => x.SchemeId)
            .HasColumnName("scheme_id")
            .IsRequired();

        builder.Property(x => x.ReferenceNo)
            .HasColumnName("reference_no")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.ReferenceNo)
            .IsUnique()
            .HasDatabaseName("UQ_FASApplications_ReferenceNo");

        builder.Property(x => x.Channel)
            .HasColumnName("channel")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.HouseholdIncome)
            .HasColumnName("household_income")
            .HasPrecision(18, 2);

        builder.Property(x => x.MlRiskClassification)
            .HasColumnName("ml_risk_classification")
            .HasColumnType("varchar(20)")
            .IsUnicode(false);

        builder.Property(x => x.RejectionReason)
            .HasColumnName("rejection_reason")
            .HasColumnType("nvarchar(500)");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<FasScheme>()
            .WithMany()
            .HasForeignKey(x => x.SchemeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
