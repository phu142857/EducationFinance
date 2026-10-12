using AdminPortal.Domain.Modules.Finance.Enums;

namespace AdminPortal.Domain.Modules.Finance.Entities;

public class FeeAssessment
{
    public Guid AssessmentId { get; set; }
    public Guid AccountId { get; set; }
    public decimal TotalBilledAmount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public FeeAssessmentPaymentStatus PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
