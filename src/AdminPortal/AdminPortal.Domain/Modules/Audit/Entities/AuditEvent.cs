using AdminPortal.Domain.Modules.Audit.Enums;

namespace AdminPortal.Domain.Modules.Audit.Entities;

public class AuditEvent
{
    public Guid EventId { get; set; }
    public Guid? StaffUserId { get; set; }
    public AuditActionType ActionType { get; set; }
    public string TargetEntity { get; set; } = string.Empty;
    public Guid? EntityId { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; }
}
