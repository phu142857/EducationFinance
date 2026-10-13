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
    public string Status { get; set; } = string.Empty;
    public AdminUser User { get; set; } = null!;
    public ICollection<AccountTransaction> AccountTransactions { get; set; } = [];
}
