namespace AdminPortal.Domain.Entities;

public sealed class FeeConfiguration
{
    public Guid Id { get; set; }
    public Guid? SchoolId { get; set; }
    public string FeeCode { get; set; } = string.Empty;
    public string FeeName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public School? School { get; set; }
    public ICollection<FeeAssessmentItem> FeeAssessmentItems { get; set; } = [];
}
