namespace EServicePortal.Domain.Entities;

public sealed class FamilyMember
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid? ParentId { get; set; }
    public Guid? StudentId { get; set; }
    public string RelationshipType { get; set; } = string.Empty;
    public bool IsPrimaryContact { get; set; }
    public DateTime CreatedAt { get; set; }
    public Family Family { get; set; } = null!;
    public Parent? Parent { get; set; }
    public StudentReadModel? Student { get; set; }
}
