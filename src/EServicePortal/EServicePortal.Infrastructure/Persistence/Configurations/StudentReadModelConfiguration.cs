using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class StudentReadModelConfiguration : IEntityTypeConfiguration<StudentReadModel>
{
    public void Configure(EntityTypeBuilder<StudentReadModel> builder)
    {
        builder.ToTable("StudentReadModels");
        builder.HasKey(x => x.StudentId);
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.StudentNumber).HasColumnType("nvarchar(60)").HasMaxLength(60).IsRequired(false);
        builder.Property(x => x.DisplayName).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired(false);
        builder.Property(x => x.EnrollmentStatus).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired(false);
        builder.Property(x => x.SyncedAt).HasColumnType("datetime2").IsRequired();
    }
}
