namespace AdminPortal.Domain.Entities;

public sealed class Student
{
    public Guid Id { get; set; }
    public Guid? CurrentClassId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateOnly? DateOfBirth { get; set; }
    public string EnrollmentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Class? CurrentClass { get; set; }
    public EducationAccount? EducationAccount { get; set; }
    public ICollection<FeeAssessment> FeeAssessments { get; set; } = [];
    public ICollection<FasApplication> FasApplications { get; set; } = [];
}
