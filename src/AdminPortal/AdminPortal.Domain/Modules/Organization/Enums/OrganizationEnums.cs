namespace AdminPortal.Domain.Modules.Organization.Enums;

public enum StudentEnrollmentStatus
{
    Unknown = 0,
    Active = 1,
    Inactive = 2,
    Transferred = 3,
    Graduated = 4,
    DroppedOut = 5
}

public enum RelationshipType
{
    Unknown = 0,
    Parent = 1,
    Guardian = 2,
    Other = 3
}
