namespace AdminPortal.Domain.Entities;

public sealed class EducationAccount
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? OpenedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public Student Student { get; set; } = null!;
    public ICollection<AccountTransaction> AccountTransactions { get; set; } = [];
}
