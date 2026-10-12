using AdminPortal.Domain.Modules.Finance.Enums;

namespace AdminPortal.Domain.Modules.Finance.Entities;

public class Budget
{
    public Guid BudgetId { get; set; }
    public Guid? SchoolId { get; set; }
    public BudgetType BudgetType { get; set; }
    public decimal AllocatedAmount { get; set; }
    public decimal UtilizedAmount { get; set; }
    public int FiscalYear { get; set; }
    public DateTime UpdatedAt { get; set; }
}
