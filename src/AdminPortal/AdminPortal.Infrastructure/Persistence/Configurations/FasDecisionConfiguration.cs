using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasDecisionConfiguration : IEntityTypeConfiguration<FasDecision>
{
    public void Configure(EntityTypeBuilder<FasDecision> builder)
    {
        builder.ToTable("FasDecisions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.FasApplicationId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.Decision).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Remarks).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.Property(x => x.DecidedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.DecidedByUserId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.HasOne(x => x.FasApplication).WithMany(x => x.Decisions).HasForeignKey(x => x.FasApplicationId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.School).WithMany(x => x.FasDecisions).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.DecidedByUser).WithMany(x => x.FasDecisions).HasForeignKey(x => x.DecidedByUserId).OnDelete(DeleteBehavior.NoAction);
    }
}
