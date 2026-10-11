using EducationFinance.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.RoleId);

        builder.Property(x => x.RoleId)
            .HasColumnName("role_id")
            .UseIdentityColumn();

        builder.Property(x => x.RoleCode)
            .HasColumnName("role_code")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.RoleCode)
            .IsUnique()
            .HasDatabaseName("UQ_Roles_RoleCode");

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("nvarchar(255)");

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");
    }
}
