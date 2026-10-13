using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class BudgetTransactionConfiguration : IEntityTypeConfiguration<BudgetTransaction>
{
    public void Configure(EntityTypeBuilder<BudgetTransaction> builder)
    {
        builder.ToTable("BudgetTransactions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.BudgetId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.BusinessReference).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.MovementType).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.OccurredAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.BusinessReference).IsUnique();
        builder.HasOne(x => x.Budget).WithMany(x => x.Transactions).HasForeignKey(x => x.BudgetId).OnDelete(DeleteBehavior.NoAction);
    }
}
