using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class StudentReadModel
{
    public Guid StudentId { get; set; }
    public Guid? SchoolId { get; set; }
    public string? StudentNumber { get; set; }
    public string? DisplayName { get; set; }
    public EnrollmentStatus? EnrollmentStatus { get; set; }
    public DateTime SyncedAt { get; set; }
    public ICollection<FamilyMember> FamilyMemberships { get; set; } = [];
    public ICollection<EducationAccountReadModel> EducationAccounts { get; set; } = [];
    public ICollection<FeeAssessmentReadModel> FeeAssessments { get; set; } = [];
}
