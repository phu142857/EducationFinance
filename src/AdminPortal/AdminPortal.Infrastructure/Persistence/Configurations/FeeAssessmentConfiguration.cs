using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FeeAssessmentConfiguration : IEntityTypeConfiguration<FeeAssessment>
{
    public void Configure(EntityTypeBuilder<FeeAssessment> builder)
    {
        builder.ToTable("FeeAssessments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AcademicYear).HasColumnType("int").IsRequired();
        builder.Property(x => x.TermCode).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.SubsidyAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.PayableAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => new { x.StudentId, x.AcademicYear, x.TermCode });
        builder.HasOne(x => x.Student).WithMany(x => x.FeeAssessments).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
