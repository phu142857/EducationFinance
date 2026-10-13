using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.CurrentClassId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.StudentNumber).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.FullName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.DateOfBirth).HasColumnType("date").IsRequired(false);
        builder.Property(x => x.Status).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired(false);
        builder.Property(x => x.EnrollmentStatus).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.StudentNumber).IsUnique();
        builder.HasOne(x => x.CurrentClass).WithMany(x => x.Students).HasForeignKey(x => x.CurrentClassId).OnDelete(DeleteBehavior.NoAction);
    }
}
