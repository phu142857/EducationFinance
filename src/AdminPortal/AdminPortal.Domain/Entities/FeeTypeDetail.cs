namespace AdminPortal.Domain.Entities;

public sealed class FeeTypeDetail
{
    public Guid Id { get; set; }
    public Guid? FeeTypeId { get; set; }
    public string FeeName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public FeeType? FeeType { get; set; }
}
