namespace AdminPortal.Domain.Entities;

public sealed class FasDecision
{
    public Guid Id { get; set; }
    public Guid FasApplicationId { get; set; }
    public Guid? SchoolId { get; set; }
    public string Decision { get; set; } = string.Empty;
    public string? Remarks { get; set; }
    public DateTime? DecidedAt { get; set; }
    public Guid? DecidedByUserId { get; set; }
    public FasApplication FasApplication { get; set; } = null!;
    public School? School { get; set; }
    public AdminUser? DecidedByUser { get; set; }
}
