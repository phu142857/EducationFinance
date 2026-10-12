namespace AdminPortal.Domain.Modules.Finance.Enums;

public enum FeeType
{
    Unknown = 0,
    SchoolFee = 1,
    MiscellaneousFee = 2,
    SecondTierMiscellaneousFee = 3,
    ExaminationFee = 4
}

public enum FeeAssessmentPaymentStatus
{
    Unknown = 0,
    Outstanding = 1,
    PartiallyPaid = 2,
    Paid = 3,
    Waived = 4,
    Cancelled = 5,
    Overdue = 6
}

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

public enum BudgetType
{
    Unknown = 0,
    NationalMoefas = 1,
    SchoolSbfas = 2,
    Other = 3
}
