using AdminPortal.Domain.Modules.Organization.Enums;

namespace AdminPortal.Domain.Modules.Organization.Entities;

public class FamilyLink
{
    public Guid LinkId { get; set; }
    public Guid ExternalParentId { get; set; }
    public Guid StudentId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public bool IsPrimary { get; set; }
}
