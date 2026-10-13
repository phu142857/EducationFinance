using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasAssessmentConfiguration : IEntityTypeConfiguration<FasAssessment>
{
    public void Configure(EntityTypeBuilder<FasAssessment> builder)
    {
        builder.ToTable("FasAssessments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FasApplicationId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.FasSchemeId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AssessmentOutcome).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.AssessedAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired(false);
        builder.Property(x => x.AssessedAt).HasColumnType("datetime2").IsRequired(false);
        builder.HasOne(x => x.FasApplication).WithMany(x => x.Assessments).HasForeignKey(x => x.FasApplicationId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.FasScheme).WithMany(x => x.Assessments).HasForeignKey(x => x.FasSchemeId).OnDelete(DeleteBehavior.NoAction);
    }
}
