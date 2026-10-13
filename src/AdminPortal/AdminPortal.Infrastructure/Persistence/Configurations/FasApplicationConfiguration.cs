using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class FasApplicationConfiguration : IEntityTypeConfiguration<FasApplication>
{
    public void Configure(EntityTypeBuilder<FasApplication> builder)
    {
        builder.ToTable("FasApplications");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.ExternalApplicationId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.ExternalFamilyId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.StudentId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.ApplicationReference).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
        builder.Property(x => x.SubmissionChannel).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.SubmittedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(x => x.ApplicationReference).IsUnique();
        builder.HasOne(x => x.Student).WithMany(x => x.FasApplications).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
    }
}
