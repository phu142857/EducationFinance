using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class TopUpRule
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string RuleName { get; set; } = string.Empty;
    public string? EligibilityDefinition { get; set; }
    public decimal Amount { get; set; }
    public DateOnly? EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public TopUpRuleStatus Status { get; set; }
    public AdminUser User { get; set; } = null!;
    public ICollection<TopUpRuleDetail> Details { get; set; } = [];
    public ICollection<AccountTransaction> AccountTransactions { get; set; } = [];
}
