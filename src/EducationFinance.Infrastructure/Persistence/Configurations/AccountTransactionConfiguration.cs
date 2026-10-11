using EducationFinance.Domain.Modules.Education.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class AccountTransactionConfiguration
    : IEntityTypeConfiguration<AccountTransaction>
{
    public void Configure(EntityTypeBuilder<AccountTransaction> builder)
    {
        builder.ToTable("AccountTransactions");

        builder.HasKey(x => x.TransactionId);

        builder.Property(x => x.TransactionId)
            .HasColumnName("transaction_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id")
            .IsRequired();

        builder.Property(x => x.TransactionType)
            .HasColumnName("transaction_type")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasColumnName("amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.BalanceAfter)
            .HasColumnName("balance_after")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.ReferenceNo)
            .HasColumnName("reference_no")
            .HasColumnType("varchar(50)")
            .IsUnicode(false);

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("nvarchar(255)");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<EducationAccount>()
            .WithMany()
            .HasForeignKey(x => x.AccountId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
