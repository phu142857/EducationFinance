namespace EServicePortal.Domain.Modules.EducationAccounts.Enums;

public enum EducationAccountStatus
{
    Unknown = 0,
    Pending = 1,
    Active = 2,
    Frozen = 3,
    Extended = 4,
    Closed = 5
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

public enum ChargeStatus
{
    Unknown = 0,
    Outstanding = 1,
    PartiallyPaid = 2,
    Paid = 3,
    Waived = 4,
    Cancelled = 5,
    Overdue = 6
}
