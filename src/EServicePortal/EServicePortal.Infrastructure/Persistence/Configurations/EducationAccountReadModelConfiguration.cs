using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class EducationAccountReadModelConfiguration : IEntityTypeConfiguration<EducationAccountReadModel>
{
    public void Configure(EntityTypeBuilder<EducationAccountReadModel> builder)
    {
        builder.ToTable("EducationAccountReadModels");
        builder.HasKey(x => x.EducationAccountId);
        builder.Property(x => x.EducationAccountId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AccountNumberMasked).HasColumnType("nvarchar(60)").HasMaxLength(60).IsRequired(false);
        builder.Property(x => x.Balance).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.CurrencyCode).HasColumnType("char(3)").HasMaxLength(3).HasDefaultValue("SGD").IsRequired();
        builder.Property(x => x.AccountStatus).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.SyncedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.StudentId);
        builder.HasOne(x => x.Student).WithMany(x => x.EducationAccounts).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
