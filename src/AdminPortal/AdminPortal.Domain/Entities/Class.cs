namespace AdminPortal.Domain.Entities;

public sealed class Class
{
    public Guid Id { get; set; }
    public Guid CohortId { get; set; }
    public string ClassCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int AcademicYear { get; set; }
    public Cohort Cohort { get; set; } = null!;
    public ICollection<Student> Students { get; set; } = [];
    public ICollection<StudentEnrollment> StudentEnrollments { get; set; } = [];
}
