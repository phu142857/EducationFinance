namespace AdminPortal.Domain.Entities;

public sealed class Budget
{
    public Guid Id { get; set; }
    public Guid FasSchemeId { get; set; }
    public Guid? SchoolId { get; set; }
    public int FiscalYear { get; set; }
    public decimal AllocatedAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public FasScheme FasScheme { get; set; } = null!;
    public School? School { get; set; }
    public ICollection<BudgetTransaction> Transactions { get; set; } = [];
}
