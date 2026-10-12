using AdminPortal.Domain.Modules.Finance.Enums;

namespace AdminPortal.Domain.Modules.Finance.Entities;

public class FeeConfiguration
{
    public Guid ConfigurationId { get; set; }
    public Guid? CohortId { get; set; }
    public FeeType FeeType { get; set; }
    public string ComponentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string AcademicTerm { get; set; } = string.Empty;
    public DateOnly PaymentDeadline { get; set; }
}
