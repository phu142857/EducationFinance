using EServicePortal.Domain.Modules.Notifications.Enums;

namespace EServicePortal.Domain.Modules.Notifications.Entities;

public sealed class ParentNotification
{
    public Guid NotificationId { get; set; }
    public Guid ParentId { get; set; }
    public NotificationType NotificationType { get; set; }
    public string Subject { get; set; } = string.Empty;
    public NotificationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
}
