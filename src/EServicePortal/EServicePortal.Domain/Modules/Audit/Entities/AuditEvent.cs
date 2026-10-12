using EServicePortal.Domain.Modules.Audit.Enums;

namespace EServicePortal.Domain.Modules.Audit.Entities;

public sealed class AuditEvent
{
    public Guid EventId { get; set; }
    public Guid? ParentId { get; set; }
    public AuditActionType ActionType { get; set; }
    public string TargetEntity { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}
