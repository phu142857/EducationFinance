using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class EducationAccountConfiguration : IEntityTypeConfiguration<EducationAccount>
{
    public void Configure(EntityTypeBuilder<EducationAccount> builder)
    {
        builder.ToTable("EducationAccounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AccountNumber).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Balance).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.OpenedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.ClosedAt).HasColumnType("datetime2").IsRequired(false);
        builder.HasIndex(x => x.StudentId).IsUnique();
        builder.HasIndex(x => x.AccountNumber).IsUnique();
        builder.HasOne(x => x.Student).WithOne(x => x.EducationAccount).HasForeignKey<EducationAccount>(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
