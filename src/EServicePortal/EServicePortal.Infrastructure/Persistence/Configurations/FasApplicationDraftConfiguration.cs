using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EServicePortal.Infrastructure.Persistence.Configurations;

public sealed class FasApplicationDraftConfiguration : IEntityTypeConfiguration<FasApplicationDraft>
{
    public void Configure(EntityTypeBuilder<FasApplicationDraft> builder)
    {
        builder.ToTable("FasApplicationDrafts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FamilyId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.CreatedByParentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.DraftPayload).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.DraftVersion).HasColumnType("int").HasDefaultValue(1).IsRequired();
        builder.Property(x => x.DraftStatus).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.SubmittedApplicationId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.UpdatedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => new { x.FamilyId, x.DraftStatus });
        builder.HasOne(x => x.Family).WithMany(x => x.FasApplicationDrafts).HasForeignKey(x => x.FamilyId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.CreatedByParent).WithMany(x => x.FasApplicationDrafts).HasForeignKey(x => x.CreatedByParentId).OnDelete(DeleteBehavior.NoAction);
    }
}
