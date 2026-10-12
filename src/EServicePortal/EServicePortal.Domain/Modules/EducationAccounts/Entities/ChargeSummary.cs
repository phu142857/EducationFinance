using EServicePortal.Domain.Modules.EducationAccounts.Enums;

namespace EServicePortal.Domain.Modules.EducationAccounts.Entities;

public sealed class ChargeSummary
{
    public Guid ChargeId { get; set; }
    public Guid AccountId { get; set; }
    public Guid ExternalAssessmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal OutstandingAmount { get; set; }
    public ChargeStatus Status { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
