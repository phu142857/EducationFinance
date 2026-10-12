using EServicePortal.Domain.Modules.EducationAccounts.Enums;

namespace EServicePortal.Domain.Modules.EducationAccounts.Entities;

public sealed class EducationAccountSummary
{
    public Guid AccountId { get; set; }
    public Guid ChildId { get; set; }
    public string AccountNumberMasked { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public EducationAccountStatus Status { get; set; }
    public DateTime LastSyncedAt { get; set; }
}
