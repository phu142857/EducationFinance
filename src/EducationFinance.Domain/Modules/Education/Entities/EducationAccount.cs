namespace EducationFinance.Domain.Modules.Education.Entities;

public class EducationAccount
{
    public Guid AccountId { get; set; }
    public Guid StudentId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateOnly? AutoCreatedAt { get; set; }
    public DateOnly? ScheduledClosureDate { get; set; }
    public string? CpfTransferStatus { get; set; }
}
