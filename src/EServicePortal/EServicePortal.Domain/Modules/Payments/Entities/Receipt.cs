namespace EServicePortal.Domain.Modules.Payments.Entities;

public sealed class Receipt
{
    public Guid ReceiptId { get; set; }
    public Guid PaymentId { get; set; }
    public string ReferenceNumber { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime IssuedAt { get; set; }
}
