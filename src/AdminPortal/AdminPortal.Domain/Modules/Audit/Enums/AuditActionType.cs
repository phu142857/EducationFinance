namespace AdminPortal.Domain.Modules.Audit.Enums;

public enum AuditActionType
{
    Unknown = 0,
    Created = 1,
    Updated = 2,
    Deleted = 3,
    Submitted = 4,
    Approved = 5,
    Rejected = 6,
    Authenticated = 7,
    Paid = 8,
    Exported = 9,
    Simulated = 10,
    Other = 11
}
