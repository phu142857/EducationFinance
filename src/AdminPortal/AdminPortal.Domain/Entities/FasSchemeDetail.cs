namespace AdminPortal.Domain.Entities;

public sealed class FasSchemeDetail
{
    public Guid Id { get; set; }
    public Guid FasSchemeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quator { get; set; }
    public decimal Value { get; set; }
    public FasScheme FasScheme { get; set; } = null!;
}
