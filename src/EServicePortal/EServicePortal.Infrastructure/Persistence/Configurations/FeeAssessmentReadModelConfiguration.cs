using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class FeeAssessmentReadModelConfiguration : IEntityTypeConfiguration<FeeAssessmentReadModel>
{
    public void Configure(EntityTypeBuilder<FeeAssessmentReadModel> builder)
    {
        builder.ToTable("FeeAssessmentReadModels");
        builder.HasKey(x => x.FeeAssessmentId);
        builder.Property(x => x.FeeAssessmentId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AcademicYear).HasColumnType("int").IsRequired(false);
        builder.Property(x => x.TermCode).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired(false);
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.SubsidyAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.PayableAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.Property(x => x.AssessmentStatus).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.SyncedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.StudentId);
        builder.HasOne(x => x.Student).WithMany(x => x.FeeAssessments).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
