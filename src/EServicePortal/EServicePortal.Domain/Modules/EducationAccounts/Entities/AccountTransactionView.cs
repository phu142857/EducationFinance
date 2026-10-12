using EServicePortal.Domain.Modules.EducationAccounts.Enums;

namespace EServicePortal.Domain.Modules.EducationAccounts.Entities;

public sealed class AccountTransactionView
{
    public Guid TransactionId { get; set; }
    public Guid AccountId { get; set; }
    public AccountTransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; }
}
