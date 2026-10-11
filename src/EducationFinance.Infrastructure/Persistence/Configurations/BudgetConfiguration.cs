using EducationFinance.Domain.Modules.Finance.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(x => x.BudgetId);

        builder.Property(x => x.BudgetId)
            .HasColumnName("budget_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id");

        builder.Property(x => x.BudgetType)
            .HasColumnName("budget_type")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.AllocatedAmount)
            .HasColumnName("allocated_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.UtilizedAmount)
            .HasColumnName("utilized_amount")
            .HasPrecision(18, 2)
            .HasDefaultValue(0.00m)
            .IsRequired();

        builder.Property(x => x.FiscalYear)
            .HasColumnName("fiscal_year")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
