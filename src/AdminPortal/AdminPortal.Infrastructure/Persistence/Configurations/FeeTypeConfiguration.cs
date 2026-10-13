using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FeeTypeConfiguration : IEntityTypeConfiguration<FeeType>
{
    public void Configure(EntityTypeBuilder<FeeType> builder)
    {
        builder.ToTable("FeeType");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.FeeName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.Property(x => x.EffectiveFrom).HasColumnType("date").IsRequired();
        builder.Property(x => x.EffectiveTo).HasColumnType("date").IsRequired(false);
        builder.HasOne(x => x.School).WithMany(x => x.FeeTypes).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
    }
}
