namespace EducationFinance.Domain.Modules.Finance.Entities;

public class FeeAssessment
{
    public Guid AssessmentId { get; set; }
    public Guid StudentId { get; set; }
    public Guid AccountId { get; set; }
    public string Term { get; set; } = string.Empty;
    public decimal TotalBilledAmount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
