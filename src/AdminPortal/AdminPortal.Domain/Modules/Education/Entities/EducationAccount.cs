using AdminPortal.Domain.Modules.Education.Enums;

namespace AdminPortal.Domain.Modules.Education.Entities;

public class EducationAccount
{
    public Guid AccountId { get; set; }
    public Guid StudentId { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public EducationAccountStatus Status { get; set; }
    public DateOnly? AutoCreatedAt { get; set; }
    public DateOnly? ScheduledClosureDate { get; set; }
    public CpfTransferStatus? CpfTransferStatus { get; set; }
}
