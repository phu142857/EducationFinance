using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(x => x.StudentId);

        builder.Property(x => x.StudentId)
            .HasColumnName("student_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id")
            .IsRequired();

        builder.Property(x => x.ClassId)
            .HasColumnName("class_id");

        builder.Property(x => x.BirthCertNumber)
            .HasColumnName("birth_cert_number")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.BirthCertNumber)
            .IsUnique()
            .HasDatabaseName("UQ_Students_BirthCertNumber");

        builder.Property(x => x.FullName)
            .HasColumnName("full_name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.DateOfBirth)
            .HasColumnName("date_of_birth")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(x => x.Citizenship)
            .HasColumnName("citizenship")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.EnrollmentStatus)
            .HasColumnName("enrollment_status")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<School>()
            .WithMany()
            .HasForeignKey(x => x.SchoolId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Class>()
            .WithMany()
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
