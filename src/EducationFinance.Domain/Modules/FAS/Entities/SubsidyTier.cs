namespace EducationFinance.Domain.Modules.FAS.Entities;

public class SubsidyTier
{
    public Guid TierId { get; set; }
    public Guid SchemeId { get; set; }
    public string TierName { get; set; } = string.Empty;
    public string FeeComponentName { get; set; } = string.Empty;
    public decimal? SubsidyPercentage { get; set; }
    public decimal? FixedSubsidyAmount { get; set; }
}
