using EServicePortal.Domain.Modules.Payments.Enums;

namespace EServicePortal.Domain.Modules.Payments.Entities;

public sealed class Payment
{
    public Guid PaymentId { get; set; }
    public Guid ParentId { get; set; }
    public Guid ChargeId { get; set; }
    public Guid? ExternalAccountId { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;
    public string? ProviderReference { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
