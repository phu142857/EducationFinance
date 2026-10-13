using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class FasApplicationReadModel
{
    public Guid FasApplicationId { get; set; }
    public Guid FamilyId { get; set; }
    public string? ApplicationReference { get; set; }
    public FasApplicationStatus ApplicationStatus { get; set; }
    public string? OutcomeSummary { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? LastStatusChangedAt { get; set; }
    public DateTime SyncedAt { get; set; }
    public Family Family { get; set; } = null!;
}
