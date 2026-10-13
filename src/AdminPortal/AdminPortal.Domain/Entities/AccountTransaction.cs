using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class AccountTransaction
{
    public Guid Id { get; set; }
    public Guid EducationAccountId { get; set; }
    public Guid? TopUpRuleId { get; set; }
    public string BusinessReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime OccurredAt { get; set; }
    public AccountTransactionType? Type { get; set; }
    public EducationAccount EducationAccount { get; set; } = null!;
    public TopUpRule? TopUpRule { get; set; }
}
