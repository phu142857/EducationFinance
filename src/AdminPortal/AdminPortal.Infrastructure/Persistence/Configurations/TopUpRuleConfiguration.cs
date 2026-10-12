using AdminPortal.Domain.Modules.Education.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class TopUpRuleConfiguration : IEntityTypeConfiguration<TopUpRule>
{
    public void Configure(EntityTypeBuilder<TopUpRule> builder)
    {
        builder.ToTable("TopUpRules");

        builder.HasKey(x => x.RuleId);

        builder.Property(x => x.RuleId)
            .HasColumnName("rule_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.RuleName)
            .HasColumnName("rule_name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.TargetAge)
            .HasColumnName("target_age");

        builder.Property(x => x.TargetLevel)
            .HasColumnName("target_level")
            .HasColumnType("varchar(50)")
            .IsUnicode(false);

        builder.Property(x => x.TopUpAmount)
            .HasColumnName("top_up_amount")
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Frequency)
            .HasColumnName("frequency")
            .HasColumnType("varchar(30)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");
    }
}
