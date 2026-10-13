namespace AdminPortal.Domain.Entities;

public sealed class FasSchemeDetail
{
    public Guid Id { get; set; }
    public Guid FasSchemeId { get; set; }
    public string RuleDefinition { get; set; } = string.Empty;
    public DateOnly? EffectiveFrom { get; set; }
    public DateOnly? EffectiveTo { get; set; }
    public FasScheme FasScheme { get; set; } = null!;
}
