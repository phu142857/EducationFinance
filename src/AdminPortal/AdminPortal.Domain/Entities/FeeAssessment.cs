using AdminPortal.Domain.Enums;

namespace AdminPortal.Domain.Entities;

public sealed class FeeAssessment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid FeeTypeId { get; set; }
    public int AcademicYear { get; set; }
    public decimal Amount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public Guid Paid { get; set; }
    public FeeAssessmentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Student Student { get; set; } = null!;
    public FeeType FeeType { get; set; } = null!;
}
