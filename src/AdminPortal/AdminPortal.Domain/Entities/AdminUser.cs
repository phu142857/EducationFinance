namespace AdminPortal.Domain.Entities;

public sealed class AdminUser
{
    public Guid Id { get; set; }
    public string ExternalIdentityId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Guid? SchoolId { get; set; }
    public string Status { get; set; } = string.Empty;
    public School? School { get; set; }
    public ICollection<TopUpRule> TopUpRules { get; set; } = [];
    public ICollection<FasDecision> FasDecisions { get; set; } = [];
    public ICollection<HardcopyFasRecord> HardcopyFasRecords { get; set; } = [];
}
