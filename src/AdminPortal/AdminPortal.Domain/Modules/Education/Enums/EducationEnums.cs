namespace AdminPortal.Domain.Modules.Education.Enums;

public enum EducationAccountStatus
{
    Unknown = 0,
    Pending = 1,
    Active = 2,
    Frozen = 3,
    Extended = 4,
    Closed = 5
}

public enum CpfTransferStatus
{
    Unknown = 0,
    NotRequired = 1,
    Pending = 2,
    Processing = 3,
    Succeeded = 4,
    Failed = 5
}

public enum AccountTransactionType
{
    Unknown = 0,
    TopUp = 1,
    FeePayment = 2,
    Refund = 3,
    CpfTransfer = 4,
    Adjustment = 5
}
