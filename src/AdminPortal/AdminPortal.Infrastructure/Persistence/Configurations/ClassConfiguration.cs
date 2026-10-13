using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.CohortId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.ClassCode).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Name).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.AcademicYear).HasColumnType("int").IsRequired();
        builder.HasIndex(x => new { x.CohortId, x.ClassCode }).IsUnique();
        builder.HasOne(x => x.Cohort).WithMany(x => x.Classes).HasForeignKey(x => x.CohortId).OnDelete(DeleteBehavior.NoAction);
    }
}
