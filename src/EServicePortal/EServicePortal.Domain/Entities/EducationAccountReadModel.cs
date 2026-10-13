using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class EducationAccountReadModel
{
    public Guid EducationAccountId { get; set; }
    public Guid StudentId { get; set; }
    public string? AccountNumberMasked { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public EducationAccountStatus AccountStatus { get; set; }
    public DateTime SyncedAt { get; set; }
    public StudentReadModel Student { get; set; } = null!;
    public ICollection<PaymentRequest> PaymentRequests { get; set; } = [];
}
