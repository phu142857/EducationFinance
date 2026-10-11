using EducationFinance.Domain.Modules.Education.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class EducationAccountConfiguration
    : IEntityTypeConfiguration<EducationAccount>
{
    public void Configure(EntityTypeBuilder<EducationAccount> builder)
    {
        builder.ToTable("EducationAccounts");

        builder.HasKey(x => x.AccountId);

        builder.Property(x => x.AccountId)
            .HasColumnName("account_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.StudentId)
            .HasColumnName("student_id")
            .IsRequired();

        builder.Property(x => x.AccountNumber)
            .HasColumnName("account_number")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.AccountNumber)
            .IsUnique()
            .HasDatabaseName("UQ_EducationAccounts_AccountNumber");

        builder.Property(x => x.Balance)
            .HasColumnName("balance")
            .HasPrecision(18, 2)
            .HasDefaultValue(0.00m)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.AutoCreatedAt)
            .HasColumnName("auto_created_at")
            .HasColumnType("date");

        builder.Property(x => x.ScheduledClosureDate)
            .HasColumnName("scheduled_closure_date")
            .HasColumnType("date");

        builder.Property(x => x.CpfTransferStatus)
            .HasColumnName("cpf_transfer_status")
            .HasColumnType("varchar(30)")
            .IsUnicode(false);

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
