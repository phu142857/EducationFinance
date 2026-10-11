namespace EducationFinance.Domain.Modules.Organization.Entities;

public class Cohort
{
    public Guid CohortId { get; set; }
    public Guid SchoolId { get; set; }
    public int CohortYear { get; set; }
    public string LevelName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
