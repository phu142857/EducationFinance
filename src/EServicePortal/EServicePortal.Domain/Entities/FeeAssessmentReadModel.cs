namespace EServicePortal.Domain.Entities;

public sealed class FeeAssessmentReadModel
{
    public Guid FeeAssessmentId { get; set; }
    public Guid StudentId { get; set; }
    public int? AcademicYear { get; set; }
    public string? TermCode { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public string AssessmentStatus { get; set; } = string.Empty;
    public DateTime SyncedAt { get; set; }
    public StudentReadModel Student { get; set; } = null!;
    public ICollection<PaymentRequest> PaymentRequests { get; set; } = [];
}
