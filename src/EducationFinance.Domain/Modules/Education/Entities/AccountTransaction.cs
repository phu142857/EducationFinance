namespace EducationFinance.Domain.Modules.Education.Entities;

public class AccountTransaction
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal BalanceAfter { get; set; }
    public string? ReferenceNo { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
