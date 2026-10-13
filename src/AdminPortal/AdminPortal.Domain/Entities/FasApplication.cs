namespace AdminPortal.Domain.Entities;

public sealed class FasApplication
{
    public Guid Id { get; set; }
    public Guid? ExternalApplicationId { get; set; }
    public Guid? ExternalFamilyId { get; set; }
    public Guid StudentId { get; set; }
    public string ApplicationReference { get; set; } = string.Empty;
    public string SubmissionChannel { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? SubmittedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public Student Student { get; set; } = null!;
    public ICollection<FasAssessment> Assessments { get; set; } = [];
    public ICollection<FasDecision> Decisions { get; set; } = [];
    public ICollection<HardcopyFasRecord> HardcopyRecords { get; set; } = [];
}
