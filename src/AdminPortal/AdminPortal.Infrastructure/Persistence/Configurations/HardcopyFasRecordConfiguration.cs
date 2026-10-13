using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class HardcopyFasRecordConfiguration : IEntityTypeConfiguration<HardcopyFasRecord>
{
    public void Configure(EntityTypeBuilder<HardcopyFasRecord> builder)
    {
        builder.ToTable("HardcopyFasRecords");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FasApplicationId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.DocumentReference).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.CapturedByUserId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.CapturedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.VerificationStatus).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.HasOne(x => x.FasApplication).WithMany(x => x.HardcopyRecords).HasForeignKey(x => x.FasApplicationId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.School).WithMany(x => x.HardcopyFasRecords).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.CapturedByUser).WithMany(x => x.HardcopyFasRecords).HasForeignKey(x => x.CapturedByUserId).OnDelete(DeleteBehavior.NoAction);
    }
}
