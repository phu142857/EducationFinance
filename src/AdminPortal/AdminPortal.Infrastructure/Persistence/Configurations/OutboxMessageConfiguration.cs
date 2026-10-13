using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPortal.Infrastructure.Persistence.Configurations;

public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier");
        builder.Property(x => x.MessageId).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(x => x.MessageType).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
        builder.Property(x => x.MessageVersion).HasColumnType("int").HasDefaultValue(1).IsRequired();
        builder.Property(x => x.Payload).HasColumnType("nvarchar(max)").IsRequired();
        builder.Property(x => x.CorrelationId).HasColumnType("uniqueidentifier").IsRequired(false);
        builder.Property(x => x.CreatedAt).HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.PublishedAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.NextAttemptAt).HasColumnType("datetime2").IsRequired(false);
        builder.Property(x => x.Status).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
        builder.Property(x => x.RetryCount).HasColumnType("int").HasDefaultValue(0).IsRequired();
        builder.Property(x => x.LastError).HasColumnType("nvarchar(max)").IsRequired(false);
        builder.HasIndex(x => x.MessageId).IsUnique();
        builder.HasIndex(x => new { x.Status, x.NextAttemptAt });
    }
}
