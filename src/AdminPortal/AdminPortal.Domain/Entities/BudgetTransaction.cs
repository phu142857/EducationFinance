namespace AdminPortal.Domain.Entities;

public sealed class BudgetTransaction
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public string BusinessReference { get; set; } = string.Empty;
    public string MovementType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime OccurredAt { get; set; }
    public Budget Budget { get; set; } = null!;
}
