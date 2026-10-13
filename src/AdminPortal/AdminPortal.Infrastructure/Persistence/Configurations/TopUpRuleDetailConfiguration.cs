using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class TopUpRuleDetailConfiguration : IEntityTypeConfiguration<TopUpRuleDetail>
{
    public void Configure(EntityTypeBuilder<TopUpRuleDetail> builder)
    {
        builder.ToTable("TopUpRuleDetails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.TopRuleId).HasColumnName("TopRuleID").HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Quator).HasColumnType("int").IsRequired();
        builder.Property(x => x.Value).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.HasOne(x => x.TopUpRule).WithMany(x => x.Details).HasForeignKey(x => x.TopRuleId).OnDelete(DeleteBehavior.NoAction);
    }
}
