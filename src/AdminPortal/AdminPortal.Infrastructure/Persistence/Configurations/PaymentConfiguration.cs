using AdminPortal.Domain.Modules.Education.Entities;
using AdminPortal.Domain.Modules.Finance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(x => x.PaymentId);

        builder.Property(x => x.PaymentId)
            .HasColumnName("payment_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.AssessmentId)
            .HasColumnName("assessment_id")
            .IsRequired();

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id");

        builder.Property(x => x.PaymentMethod)
            .HasColumnName("payment_method")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.ReceiptReference)
            .HasColumnName("receipt_reference")
            .HasColumnType("varchar(50)")
            .IsUnicode(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<FeeAssessment>()
            .WithMany()
            .HasForeignKey(x => x.AssessmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<EducationAccount>()
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
