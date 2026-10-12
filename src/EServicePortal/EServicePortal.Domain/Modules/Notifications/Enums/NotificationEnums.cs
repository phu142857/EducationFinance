namespace EServicePortal.Domain.Modules.Notifications.Enums;

public enum NotificationType
{
    Unknown = 0,
    FasStatusChanged = 1,
    PaymentStatusChanged = 2,
    AccountStatusChanged = 3,
    FeeDueReminder = 4,
    General = 5
}

public enum NotificationStatus
{
    Unknown = 0,
    Pending = 1,
    Sent = 2,
    Failed = 3,
    Read = 4
}
