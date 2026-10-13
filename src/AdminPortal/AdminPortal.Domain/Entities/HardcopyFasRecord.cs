namespace AdminPortal.Domain.Entities;

public sealed class HardcopyFasRecord
{
    public Guid Id { get; set; }
    public Guid? FasApplicationId { get; set; }
    public Guid SchoolId { get; set; }
    public string? DocumentReference { get; set; }
    public Guid? CapturedByUserId { get; set; }
    public DateTime? CapturedAt { get; set; }
    public string VerificationStatus { get; set; } = string.Empty;
    public FasApplication? FasApplication { get; set; }
    public School School { get; set; } = null!;
    public AdminUser? CapturedByUser { get; set; }
}
