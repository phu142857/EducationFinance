using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public string MessageType { get; set; } = string.Empty;
    public int MessageVersion { get; set; }
    public string Payload { get; set; } = string.Empty;
    public Guid? CorrelationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? NextAttemptAt { get; set; }
    public OutboxMessageStatus Status { get; set; }
    public int RetryCount { get; set; }
    public string? LastError { get; set; }
}
