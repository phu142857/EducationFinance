using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FeeConfigurationConfiguration : IEntityTypeConfiguration<FeeConfiguration>
{
    public void Configure(EntityTypeBuilder<FeeConfiguration> builder)
    {
        builder.ToTable("FeeConfigurations");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.FeeCode).HasColumnType("nvarchar(50)").HasMaxLength(50).IsRequired();
        builder.Property(x => x.FeeName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EffectiveFrom).HasColumnType("date").IsRequired();
        builder.Property(x => x.EffectiveTo).HasColumnType("date").IsRequired(false);
        builder.HasOne(x => x.School).WithMany(x => x.FeeConfigurations).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
    }
}
