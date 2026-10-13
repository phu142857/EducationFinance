using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
{
    public void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        builder.ToTable("AdminUsers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.ExternalIdentityId).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
        builder.Property(x => x.DisplayName).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Email).HasColumnType("nvarchar(255)").HasMaxLength(255).IsRequired(false);
        builder.Property(x => x.SchoolId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.Status).HasConversion<string>().HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.HasIndex(x => x.ExternalIdentityId).IsUnique();
        builder.HasOne(x => x.School).WithMany(x => x.AdminUsers).HasForeignKey(x => x.SchoolId).OnDelete(DeleteBehavior.NoAction);
    }
}
