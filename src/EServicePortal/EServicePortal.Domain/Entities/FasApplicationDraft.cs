using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class FasApplicationDraft
{
    public Guid Id { get; set; }
    public Guid FamilyId { get; set; }
    public Guid CreatedByParentId { get; set; }
    public string DraftPayload { get; set; } = string.Empty;
    public int DraftVersion { get; set; }
    public FasApplicationDraftStatus DraftStatus { get; set; }
    public Guid? SubmittedApplicationId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Family Family { get; set; } = null!;
    public Parent CreatedByParent { get; set; } = null!;
}
