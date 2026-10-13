using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasSchemeDetailConfiguration : IEntityTypeConfiguration<FasSchemeDetail>
{
    public void Configure(EntityTypeBuilder<FasSchemeDetail> builder)
    {
        builder.ToTable("FasSchemesDetails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FasSchemeId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.RuleDefinition).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.EffectiveFrom).HasColumnType("date").IsRequired(false);
        builder.Property(x => x.EffectiveTo).HasColumnType("date").IsRequired(false);
        builder.HasOne(x => x.FasScheme).WithMany(x => x.Details).HasForeignKey(x => x.FasSchemeId).OnDelete(DeleteBehavior.NoAction);
    }
}
