using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class CohortConfiguration : IEntityTypeConfiguration<Cohort>
{
    public void Configure(EntityTypeBuilder<Cohort> builder)
    {
        builder.ToTable("Cohorts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.StartYear).HasColumnType("int").IsRequired();
        builder.Property(x => x.EndYear).HasColumnType("int").IsRequired();
        builder.Property(x => x.LevelName).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Name).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.HasIndex(x => new { x.SchoolId, x.LevelName, x.Name }).IsUnique();
        builder.HasOne(x => x.School).WithMany(x => x.Cohorts).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
    }
}
