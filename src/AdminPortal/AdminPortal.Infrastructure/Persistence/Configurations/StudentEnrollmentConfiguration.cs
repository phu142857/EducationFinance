using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class StudentEnrollmentConfiguration : IEntityTypeConfiguration<StudentEnrollment>
{
    public void Configure(EntityTypeBuilder<StudentEnrollment> builder)
    {
        builder.ToTable("StudentEnrollments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.ClassId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired(false);
        builder.HasOne(x => x.Student).WithMany(x => x.Enrollments).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.Class).WithMany(x => x.StudentEnrollments).HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.NoAction);
    }
}
