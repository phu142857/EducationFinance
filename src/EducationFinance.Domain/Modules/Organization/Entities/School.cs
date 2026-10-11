namespace EducationFinance.Domain.Modules.Organization.Entities;

public class School
{
    public Guid SchoolId { get; set; }
    public string SchoolCode { get; set; } = string.Empty;
    public string SchoolName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? ContactNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}
