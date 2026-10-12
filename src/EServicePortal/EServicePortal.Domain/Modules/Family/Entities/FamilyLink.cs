using EServicePortal.Domain.Modules.Family.Enums;

namespace EServicePortal.Domain.Modules.Family.Entities;

public sealed class FamilyLink
{
    public Guid LinkId { get; set; }
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
    public RelationshipType RelationshipType { get; set; }
    public bool IsPrimary { get; set; }
}
