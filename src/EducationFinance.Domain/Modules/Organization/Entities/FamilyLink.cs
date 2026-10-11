namespace EducationFinance.Domain.Modules.Organization.Entities;

public class FamilyLink
{
    public Guid LinkId { get; set; }
    public Guid ParentId { get; set; }
    public Guid StudentId { get; set; }
    public string RelationshipType { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}
