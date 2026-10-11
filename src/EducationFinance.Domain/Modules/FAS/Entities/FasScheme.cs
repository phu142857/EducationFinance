namespace EducationFinance.Domain.Modules.FAS.Entities;

public class FasScheme
{
    public Guid SchemeId { get; set; }
    public string SchemeName { get; set; } = string.Empty;
    public decimal IncomeCeiling { get; set; }
    public string? RulesJson { get; set; }
    public bool IsActive { get; set; }
}
