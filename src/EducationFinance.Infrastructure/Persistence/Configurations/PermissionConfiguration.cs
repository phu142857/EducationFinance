using EducationFinance.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(x => x.PermissionId);

        builder.Property(x => x.PermissionId)
            .HasColumnName("permission_id")
            .UseIdentityColumn();

        builder.Property(x => x.PermissionCode)
            .HasColumnName("permission_code")
            .HasColumnType("varchar(100)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.PermissionCode)
            .IsUnique()
            .HasDatabaseName("UQ_Permissions_PermissionCode");

        builder.Property(x => x.Module)
            .HasColumnName("module")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("nvarchar(255)");
    }
}
