using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class TopUpRuleConfiguration : IEntityTypeConfiguration<TopUpRule>
{
    public void Configure(EntityTypeBuilder<TopUpRule> builder)
    {
        builder.ToTable("TopUpRules");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.UserId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.RuleName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.EligibilityDefinition).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EffectiveFrom).HasColumnType("date").IsRequired(false);
        builder.Property(x => x.EffectiveTo).HasColumnType("date").IsRequired(false);
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.HasOne(x => x.User).WithMany(x => x.TopUpRules).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
    }
}
