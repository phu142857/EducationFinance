namespace EServicePortal.Domain.Modules.FinancialAssistance.Enums;

public enum FasApplicationStatus
{
    Unknown = 0,
    Draft = 1,
    Submitted = 2,
    AutoGranted = 3,
    PendingSchoolDecision = 4,
    Approved = 5,
    Rejected = 6,
    Cancelled = 7
}
