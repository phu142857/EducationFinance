using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FasSchemeId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.FiscalYear).HasColumnType("int").IsRequired();
        builder.Property(x => x.AllocatedAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.HasOne(x => x.FasScheme).WithMany(x => x.Budgets).HasForeignKey(x => x.FasSchemeId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.School).WithMany(x => x.Budgets).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
    }
}
