using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public int MessageVersion { get; set; }
    public string Payload { get; set; } = string.Empty;
    public Guid? CorrelationId { get; set; }
    public string Destination { get; set; } = string.Empty;
    public OutboxMessageStatus Status { get; set; }
    public DateTime OccurredAt { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public int AttemptCount { get; set; }
    public DateTime? LockedUntil { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? LastError { get; set; }
}
