namespace AdminPortal.Domain.Entities;

public sealed class FasAssessment
{
    public Guid Id { get; set; }
    public Guid FasApplicationId { get; set; }
    public Guid FasSchemeId { get; set; }
    public string AssessmentOutcome { get; set; } = string.Empty;
    public decimal? AssessedAmount { get; set; }
    public DateTime? AssessedAt { get; set; }
    public FasApplication FasApplication { get; set; } = null!;
    public FasScheme FasScheme { get; set; } = null!;
}
