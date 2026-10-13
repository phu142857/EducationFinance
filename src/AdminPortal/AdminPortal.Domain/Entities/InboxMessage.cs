namespace AdminPortal.Domain.Entities;

public sealed class InboxMessage
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public string ConsumerName { get; set; } = string.Empty;
    public string MessageType { get; set; } = string.Empty;
    public int MessageVersion { get; set; }
    public string Payload { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public int RetryCount { get; set; }
    public string? LastError { get; set; }
}
