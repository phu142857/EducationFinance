namespace AdminPortal.Domain.Entities;

public sealed class FasScheme
{
    public Guid Id { get; set; }
    public string SchemeCode { get; set; } = string.Empty;
    public string SchemeName { get; set; } = string.Empty;
    public string SchemeType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public ICollection<FasSchemeDetail> Details { get; set; } = [];
    public ICollection<FasAssessment> Assessments { get; set; } = [];
    public ICollection<Budget> Budgets { get; set; } = [];
}
