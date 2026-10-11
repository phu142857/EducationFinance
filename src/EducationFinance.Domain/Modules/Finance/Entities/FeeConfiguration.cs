namespace EducationFinance.Domain.Modules.Finance.Entities;

public class FeeConfiguration
{
    public Guid ConfigurationId { get; set; }
    public Guid? SchoolId { get; set; }
    public Guid? CohortId { get; set; }
    public string FeeType { get; set; } = string.Empty;
    public string ComponentName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string AcademicTerm { get; set; } = string.Empty;
}
