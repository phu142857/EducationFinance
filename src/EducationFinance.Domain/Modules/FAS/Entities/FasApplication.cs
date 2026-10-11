namespace EducationFinance.Domain.Modules.FAS.Entities;

public class FasApplication
{
    public Guid ApplicationId { get; set; }
    public Guid ParentId { get; set; }
    public Guid SchemeId { get; set; }
    public string ReferenceNo { get; set; } = string.Empty;
    public string Channel { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal? HouseholdIncome { get; set; }
    public string? MlRiskClassification { get; set; }
    public string? RejectionReason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
