using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class ClassConfiguration : IEntityTypeConfiguration<Class>
{
    public void Configure(EntityTypeBuilder<Class> builder)
    {
        builder.ToTable("Classes");

        builder.HasKey(x => x.ClassId);

        builder.Property(x => x.ClassId)
            .HasColumnName("class_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.CohortId)
            .HasColumnName("cohort_id")
            .IsRequired();

        builder.Property(x => x.ClassName)
            .HasColumnName("class_name")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.AcademicYear)
            .HasColumnName("academic_year")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne<Cohort>()
            .WithMany()
            .HasForeignKey(x => x.CohortId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
