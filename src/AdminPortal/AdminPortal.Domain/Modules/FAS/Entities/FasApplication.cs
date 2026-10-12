using AdminPortal.Domain.Modules.FAS.Enums;

namespace AdminPortal.Domain.Modules.FAS.Entities;

public class FasApplication
{
    public Guid ApplicationId { get; set; }
    public Guid ExternalParentId { get; set; }
    public Guid SchemeId { get; set; }
    public string ReferenceNo { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public FasApplicationStatus Status { get; set; }
    public decimal? HouseholdIncome { get; set; }
    public string? MlRiskClassification { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
