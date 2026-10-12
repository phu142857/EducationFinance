namespace EServicePortal.Domain.Modules.Family.Entities;

public sealed class ChildProfile
{
    public Guid ChildId { get; set; }
    public Guid ExternalStudentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid SchoolId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public bool IsEligible { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
