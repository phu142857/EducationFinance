namespace EServicePortal.Domain.Entities;

public sealed class Family
{
    public Guid Id { get; set; }
    public string? FamilyReference { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<FamilyMember> Members { get; set; } = [];
    public ICollection<FasApplicationReadModel> FasApplications { get; set; } = [];
    public ICollection<FasApplicationDraft> FasApplicationDrafts { get; set; } = [];
}
