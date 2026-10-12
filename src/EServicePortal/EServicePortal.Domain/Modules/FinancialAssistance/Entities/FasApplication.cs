using EServicePortal.Domain.Modules.FinancialAssistance.Enums;

namespace EServicePortal.Domain.Modules.FinancialAssistance.Entities;

public sealed class FasApplication
{
    public Guid ApplicationId { get; set; }
    public Guid ParentId { get; set; }
    public string? ReferenceNumber { get; set; }
    public FasApplicationStatus Status { get; set; }
    public bool IsDraft { get; set; }
    public decimal? HouseholdIncome { get; set; }
    public bool DeclarationAccepted { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
