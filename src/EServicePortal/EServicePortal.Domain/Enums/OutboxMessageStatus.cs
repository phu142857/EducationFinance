namespace EServicePortal.Domain.Enums;

public enum OutboxMessageStatus
{
    Pending,
    Processing,
    Published,
    Failed,
    DeadLetter
}
