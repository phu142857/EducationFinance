namespace EServicePortal.Infrastructure.Persistence.Messaging;

internal enum OutboxMessageType
{
    Unknown = 0,
    FasApplicationSubmittedV1 = 1,
    PaymentSettledV1 = 2,
    PaymentRefundedV1 = 3,
    ParentProfileChangedV1 = 4
}
