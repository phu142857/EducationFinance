using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FeeTypeDetailConfiguration : IEntityTypeConfiguration<FeeTypeDetail>
{
    public void Configure(EntityTypeBuilder<FeeTypeDetail> builder)
    {
        builder.ToTable("FeeTypeDetail");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FeeTypeId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.FeeName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasPrecision(18, 2).IsRequired();
        builder.HasOne(x => x.FeeType).WithMany(x => x.Details).HasForeignKey(x => x.FeeTypeId).OnDelete(DeleteBehavior.NoAction);
    }
}
