namespace EServicePortal.Domain.Modules.Payments.Enums;

public enum PaymentStatus
{
    Unknown = 0,
    Pending = 1,
    Processing = 2,
    Succeeded = 3,
    Failed = 4,
    Refunded = 5,
    Cancelled = 6
}
