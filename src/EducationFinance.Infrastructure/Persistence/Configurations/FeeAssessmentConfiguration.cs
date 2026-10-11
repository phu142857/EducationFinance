using EducationFinance.Domain.Modules.Education.Entities;
using EducationFinance.Domain.Modules.Finance.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class FeeAssessmentConfiguration
    : IEntityTypeConfiguration<FeeAssessment>
{
    public void Configure(EntityTypeBuilder<FeeAssessment> builder)
    {
        builder.ToTable("FeeAssessments");

        builder.HasKey(x => x.AssessmentId);

        builder.Property(x => x.AssessmentId)
            .HasColumnName("assessment_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.StudentId)
            .HasColumnName("student_id")
            .IsRequired();

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id")
            .IsRequired();

        builder.Property(x => x.Term)
            .HasColumnName("term")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.TotalBilledAmount)
            .HasColumnName("total_billed_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.SubsidyAmount)
            .HasColumnName("subsidy_amount")
            .HasPrecision(18, 2)
            .HasDefaultValue(0.00m)
            .IsRequired();

        builder.Property(x => x.PayableAmount)
            .HasColumnName("payable_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.PaymentStatus)
            .HasColumnName("payment_status")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<EducationAccount>()
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
