namespace EducationFinance.Domain.Modules.Finance.Entities;

public class Budget
{
    public Guid BudgetId { get; set; }
    public Guid? SchoolId { get; set; }
    public string BudgetType { get; set; } = string.Empty;
    public decimal AllocatedAmount { get; set; }
    public decimal UtilizedAmount { get; set; }
    public int FiscalYear { get; set; }
    public DateTime UpdatedAt { get; set; }
}
