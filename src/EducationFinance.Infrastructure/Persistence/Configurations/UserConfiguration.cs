using EducationFinance.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationFinance.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SingpassUinFin)
            .HasColumnName("singpass_uin_fin")
            .HasColumnType("varchar(20)")
            .IsUnicode(false);

        builder.HasIndex(x => x.SingpassUinFin)
            .IsUnique()
            .HasDatabaseName("UQ_Users_SingpassUinFin");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("nvarchar(100)")
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .HasColumnType("varchar(100)")
            .IsUnicode(false);

        builder.Property(x => x.PhoneNumber)
            .HasColumnName("phone_number")
            .HasColumnType("varchar(20)")
            .IsUnicode(false);

        builder.Property(x => x.UserType)
            .HasColumnName("user_type")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");
    }
}
