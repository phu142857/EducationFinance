namespace AdminPortal.Domain.Modules.Organization.Entities;

public class Class
{
    public Guid ClassId { get; set; }
    public Guid CohortId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
}
