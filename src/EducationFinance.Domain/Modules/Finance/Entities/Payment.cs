namespace EducationFinance.Domain.Modules.Finance.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }
    public Guid AssessmentId { get; set; }
    public Guid? AccountId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? ReceiptReference { get; set; }
    public DateTime CreatedAt { get; set; }
}
