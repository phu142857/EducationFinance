using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FeeAssessmentItemConfiguration : IEntityTypeConfiguration<FeeAssessmentItem>
{
    public void Configure(EntityTypeBuilder<FeeAssessmentItem> builder)
    {
        builder.ToTable("FeeAssessmentItems");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.Name).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.FeeAssessmentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.FeeConfigurationId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.AssessedAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.SubsidyAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0m).IsRequired();
        builder.HasOne(x => x.FeeAssessment).WithMany(x => x.Items).HasForeignKey(x => x.FeeAssessmentId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.FeeConfiguration).WithMany(x => x.FeeAssessmentItems).HasForeignKey(x => x.FeeConfigurationId).OnDelete(DeleteBehavior.NoAction);
    }
}
