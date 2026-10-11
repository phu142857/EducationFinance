namespace EducationFinance.Domain.Modules.Identity.Entities;

public class RoleAssignment
{
    public Guid AssignmentId { get; set; }
    public Guid UserId { get; set; }
    public int RoleId { get; set; }
    public Guid? SchoolId { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
