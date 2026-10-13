using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class FasApplication
{
    public Guid Id { get; set; }
    public Guid? ExternalApplicationId { get; set; }
    public Guid? ExternalFamilyId { get; set; }
    public Guid StudentId { get; set; }
    public string ApplicationReference { get; set; } = string.Empty;
    public FasApplicationStatus Status { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid FasSchemeId { get; set; }
    public AssessmentOutcome AssessmentOutcome { get; set; }
    public decimal? AssessedAmount { get; set; }
    public DateTime? AssessedAt { get; set; }
    public Student Student { get; set; } = null!;
    public FasScheme FasScheme { get; set; } = null!;
    public ICollection<FasDecision> Decisions { get; set; } = [];
    public ICollection<HardcopyFasRecord> HardcopyRecords { get; set; } = [];
}
