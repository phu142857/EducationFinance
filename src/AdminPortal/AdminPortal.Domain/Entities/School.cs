using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class School
{
    public Guid Id { get; set; }
    public string SchoolCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public SchoolStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Cohort> Cohorts { get; set; } = [];
    public ICollection<FeeType> FeeTypes { get; set; } = [];
    public ICollection<FasDecision> FasDecisions { get; set; } = [];
    public ICollection<HardcopyFasRecord> HardcopyFasRecords { get; set; } = [];
    public ICollection<Budget> Budgets { get; set; } = [];
    public ICollection<AdminUser> AdminUsers { get; set; } = [];
}
