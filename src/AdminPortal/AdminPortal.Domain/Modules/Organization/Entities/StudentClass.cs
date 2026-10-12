namespace AdminPortal.Domain.Modules.Organization.Entities;

public class StudentClass
{
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
    public DateTime AssignedAt { get; set; }
    public DateTime? EndedAt { get; set; }
}
