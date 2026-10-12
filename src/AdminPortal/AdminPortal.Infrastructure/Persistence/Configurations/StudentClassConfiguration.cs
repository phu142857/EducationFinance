using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class StudentClassConfiguration : IEntityTypeConfiguration<StudentClass>
{
    public void Configure(EntityTypeBuilder<StudentClass> builder)
    {
        builder.ToTable("StudentClasses");
        builder.HasKey(x => new { x.StudentId, x.ClassId });

        builder.Property(x => x.StudentId).HasColumnName("student_id");
        builder.Property(x => x.ClassId).HasColumnName("class_id");
        builder.Property(x => x.AssignedAt)
            .HasColumnName("assigned_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(x => x.EndedAt)
            .HasColumnName("ended_at")
            .HasColumnType("datetime2");

        builder.HasOne<Student>()
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Class>()
            .WithMany()
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
