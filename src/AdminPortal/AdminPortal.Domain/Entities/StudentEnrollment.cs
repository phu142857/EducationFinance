using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class StudentEnrollment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
    public EnrollmentStatus? Status { get; set; }
    public Student Student { get; set; } = null!;
    public Class Class { get; set; } = null!;
}
