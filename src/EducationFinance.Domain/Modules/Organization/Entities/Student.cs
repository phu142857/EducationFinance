namespace EducationFinance.Domain.Modules.Organization.Entities;

public class Student
{
    public Guid StudentId { get; set; }
    public Guid SchoolId { get; set; }
    public Guid? ClassId { get; set; }
    public string BirthCertNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Citizenship { get; set; } = string.Empty;
    public string EnrollmentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
