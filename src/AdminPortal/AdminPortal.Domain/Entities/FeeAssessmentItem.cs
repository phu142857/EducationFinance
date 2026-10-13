namespace AdminPortal.Domain.Entities;

public sealed class FeeAssessmentItem
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid FeeAssessmentId { get; set; }
    public Guid FeeConfigurationId { get; set; }
    public decimal AssessedAmount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public FeeAssessment FeeAssessment { get; set; } = null!;
    public FeeConfiguration FeeConfiguration { get; set; } = null!;
}
