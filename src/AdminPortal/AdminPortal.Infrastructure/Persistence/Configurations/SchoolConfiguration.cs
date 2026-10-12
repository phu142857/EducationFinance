using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class SchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.ToTable("Schools");

        builder.HasKey(x => x.SchoolId);

        builder.Property(x => x.SchoolId)
            .HasColumnName("school_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.SchoolCode)
            .HasColumnName("school_code")
            .HasColumnType("varchar(20)")
            .IsUnicode(false)
            .IsRequired();

        builder.HasIndex(x => x.SchoolCode)
            .IsUnique()
            .HasDatabaseName("UQ_Schools_SchoolCode");

        builder.Property(x => x.SchoolName)
            .HasColumnName("school_name")
            .HasColumnType("nvarchar(150)")
            .IsRequired();

        builder.Property(x => x.Address)
            .HasColumnName("address")
            .HasColumnType("nvarchar(255)");

        builder.Property(x => x.ContactNumber)
            .HasColumnName("contact_number")
            .HasColumnType("varchar(20)")
            .IsUnicode(false);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");
    }
}
