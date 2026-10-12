using AdminPortal.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class StaffUserConfiguration : IEntityTypeConfiguration<StaffUser>
{
    public void Configure(EntityTypeBuilder<StaffUser> builder)
    {
        builder.ToTable("StaffUsers");
        builder.HasKey(x => x.StaffUserId);
        builder.Property(x => x.StaffUserId).HasColumnName("staff_user_id").HasDefaultValueSql("NEWID()");
        builder.Property(x => x.Username).HasColumnName("username").HasColumnType("varchar(100)").IsUnicode(false).IsRequired();
        builder.HasIndex(x => x.Username).IsUnique().HasDatabaseName("UQ_StaffUsers_Username");
        builder.Property(x => x.Name).HasColumnName("name").HasColumnType("nvarchar(100)").IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasColumnType("varchar(100)").IsUnicode(false);
        builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasColumnType("varchar(20)").IsUnicode(false);
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash").HasColumnType("varchar(255)").IsUnicode(false).IsRequired();
        builder.Property(x => x.Address).HasColumnName("address").HasColumnType("nvarchar(500)");
        builder.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("datetime2").HasDefaultValueSql("SYSDATETIME()");
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime2").HasDefaultValueSql("SYSDATETIME()");
    }
}
