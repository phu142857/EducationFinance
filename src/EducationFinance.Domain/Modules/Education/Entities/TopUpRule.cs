namespace EducationFinance.Domain.Modules.Education.Entities;

public class TopUpRule
{
    public Guid RuleId { get; set; }
    public string RuleName { get; set; } = string.Empty;
    public int? TargetAge { get; set; }
    public string? TargetLevel { get; set; }
    public decimal TopUpAmount { get; set; }
    public string Frequency { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
