using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class FasScheme
{
    public Guid Id { get; set; }
    public string SchemeCode { get; set; } = string.Empty;
    public string SchemeName { get; set; } = string.Empty;
    public FasSchemeType SchemeType { get; set; }
    public FasSchemeStatus Status { get; set; }
    public FasSchemeDividedBy DividedBy { get; set; }
    public decimal? Value { get; set; }
    public ICollection<FasSchemeDetail> Details { get; set; } = [];
    public ICollection<FasApplication> Applications { get; set; } = [];
    public ICollection<Budget> Budgets { get; set; } = [];
}
