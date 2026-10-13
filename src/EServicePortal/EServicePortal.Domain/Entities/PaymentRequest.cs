using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class PaymentRequest
{
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public Guid EducationAccountId { get; set; }
    public Guid? FeeAssessmentId { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentRequestStatus Status { get; set; }
    public string? ExternalReference { get; set; }
    public string? FailureReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Parent Parent { get; set; } = null!;
    public EducationAccountReadModel EducationAccount { get; set; } = null!;
    public FeeAssessmentReadModel? FeeAssessment { get; set; }
}
