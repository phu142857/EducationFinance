using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class Budget
{
    public Guid Id { get; set; }
    public Guid FasSchemeId { get; set; }
    public Guid? SchoolId { get; set; }
    public int FiscalYear { get; set; }
    public decimal AllocatedAmount { get; set; }
    public BudgetStatus Status { get; set; }
    public FasScheme FasScheme { get; set; } = null!;
    public School? School { get; set; }
    public ICollection<BudgetTransaction> Transactions { get; set; } = [];
}
