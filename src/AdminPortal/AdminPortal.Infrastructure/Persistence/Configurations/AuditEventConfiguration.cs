using AdminPortal.Domain.Modules.Audit.Entities;
using AdminPortal.Domain.Modules.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class AuditEventConfiguration : IEntityTypeConfiguration<AuditEvent>
{
    public void Configure(EntityTypeBuilder<AuditEvent> builder)
    {
        builder.ToTable("AuditEvents");

        builder.HasKey(x => x.EventId);

        builder.Property(x => x.EventId)
            .HasColumnName("event_id")
            .HasDefaultValueSql("NEWID()");

        builder.Property(x => x.StaffUserId)
            .HasColumnName("staff_user_id");

        builder.Property(x => x.ActionType)
            .HasColumnName("action_type")
            .HasConversion<string>()
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.TargetEntity)
            .HasColumnName("target_entity")
            .HasColumnType("varchar(50)")
            .IsUnicode(false)
            .IsRequired();

        builder.Property(x => x.EntityId)
            .HasColumnName("entity_id");

        builder.Property(x => x.OldValues)
            .HasColumnName("old_values")
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.NewValues)
            .HasColumnName("new_values")
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.IpAddress)
            .HasColumnName("ip_address")
            .HasColumnType("varchar(45)")
            .IsUnicode(false);

        builder.Property(x => x.Timestamp)
            .HasColumnName("timestamp")
            .HasColumnType("datetime2")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne<StaffUser>()
            .WithMany()
            .HasForeignKey(x => x.StaffUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
