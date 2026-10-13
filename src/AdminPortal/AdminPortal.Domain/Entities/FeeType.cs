namespace AdminPortal.Domain.Entities;

public sealed class FeeType
{
    public Guid Id { get; set; }
    public Guid? SchoolId { get; set; }
    public string FeeName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public DateOnly EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public School? School { get; set; }
    public ICollection<FeeTypeDetail> Details { get; set; } = [];
    public ICollection<FeeAssessment> FeeAssessments { get; set; } = [];
}
