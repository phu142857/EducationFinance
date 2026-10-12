namespace EServicePortal.Domain.Modules.Identity.Enums;

public enum ParentAccountStatus
{
    Unknown = 0,
    PendingVerification = 1,
    Active = 2,
    Suspended = 3,
    Closed = 4
}

public enum AssistedRegistrationStatus
{
    Unknown = 0,
    Pending = 1,
    AwaitingVerification = 2,
    Verified = 3,
    Rejected = 4,
    Expired = 5,
    Revoked = 6
}
