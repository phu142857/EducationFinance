namespace EServicePortal.Infrastructure.Persistence.Messaging;

internal sealed class OutboxMessage
{
    public Guid MessageId { get; set; }
    public OutboxMessageType MessageType { get; set; }
    public string Payload { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public int RetryCount { get; set; }
    public string? LastError { get; set; }
}
