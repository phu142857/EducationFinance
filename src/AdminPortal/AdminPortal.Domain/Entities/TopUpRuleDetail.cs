namespace AdminPortal.Domain.Entities;

public sealed class TopUpRuleDetail
{
    public Guid Id { get; set; }
    public Guid TopRuleId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quator { get; set; }
    public decimal Value { get; set; }
    public TopUpRule TopUpRule { get; set; } = null!;
}
