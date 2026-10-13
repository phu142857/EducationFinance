using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class AccountTransactionConfiguration : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.ToTable("AccountTransactions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.EducationAccountId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.TopUpRuleId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.BusinessReference).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.OccurredAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.Type).HasConversion<string>().HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired(false);
        builder.HasIndex(x => x.BusinessReference).IsUnique();
        builder.HasIndex(x => new { x.EducationAccountId, x.OccurredAt });
        builder.HasOne(x => x.EducationAccount).WithMany(x => x.AccountTransactions).HasForeignKey(x => x.EducationAccountId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.TopUpRule).WithMany(x => x.AccountTransactions).HasForeignKey(x => x.TopUpRuleId).OnDelete(DeleteBehavior.NoAction);
    }
}
