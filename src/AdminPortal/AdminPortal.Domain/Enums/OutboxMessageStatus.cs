namespace AdminPortal.Domain.Enums;

public enum OutboxMessageStatus
{
    Pending,
    Processing,
    Published,
    Failed,
    DeadLetter
}
