using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class PaymentRequestConfiguration : IEntityTypeConfiguration<PaymentRequest>
{
    public void Configure(EntityTypeBuilder<PaymentRequest> builder)
    {
        builder.ToTable("PaymentRequests");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.ParentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.EducationAccountId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.FeeAssessmentId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.CurrencyCode).HasColumnType("char(3)").HasMaxLength(3).HasDefaultValue("SGD").IsRequired();
        builder.Property(x => x.PaymentMethod).HasColumnType("nvarchar(40)").HasMaxLength(40).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.ExternalReference).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired(false);
        builder.Property(x => x.FailureReason).HasColumnType("nvarchar(500)").HasMaxLength(500).IsRequired(false);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.CompletedAt).HasColumnType("datetime2").IsRequired(false);
        builder.HasIndex(x => new { x.ParentId, x.CreatedAt });
        builder.HasOne(x => x.Parent).WithMany(x => x.PaymentRequests).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.EducationAccount).WithMany(x => x.PaymentRequests).HasForeignKey(x => x.EducationAccountId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.FeeAssessment).WithMany(x => x.PaymentRequests).HasForeignKey(x => x.FeeAssessmentId).OnDelete(DeleteBehavior.NoAction);
    }
}
