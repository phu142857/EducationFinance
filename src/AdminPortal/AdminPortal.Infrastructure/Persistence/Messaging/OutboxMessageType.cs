namespace AdminPortal.Infrastructure.Persistence.Messaging;

internal enum OutboxMessageType
{
    Unknown = 0,
    EducationAccountSummaryChangedV1 = 1,
    EducationAccountTransactionPostedV1 = 2,
    FeeChargeChangedV1 = 3,
    FasApplicationStatusChangedV1 = 4,
    FamilyChildProfileChangedV1 = 5
}
