using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class FasApplicationReadModelConfiguration : IEntityTypeConfiguration<FasApplicationReadModel>
{
    public void Configure(EntityTypeBuilder<FasApplicationReadModel> builder)
    {
        builder.ToTable("FasApplicationReadModels");
        builder.HasKey(x => x.FasApplicationId);
        builder.Property(x => x.FasApplicationId).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FamilyId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.ApplicationReference).HasColumnType("nvarchar(80)").HasMaxLength(80).IsRequired(false);
        builder.Property(x => x.ApplicationStatus).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.OutcomeSummary).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.SubmittedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.LastStatusChangedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.SyncedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.FamilyId);
        builder.HasOne(x => x.Family).WithMany(x => x.FasApplications).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.NoAction);
    }
}
