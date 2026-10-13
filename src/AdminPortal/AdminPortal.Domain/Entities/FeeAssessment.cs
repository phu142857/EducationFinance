namespace AdminPortal.Domain.Entities;

public sealed class FeeAssessment
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public int AcademicYear { get; set; }
    public string TermCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public decimal SubsidyAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Student Student { get; set; } = null!;
    public ICollection<FeeAssessmentItem> Items { get; set; } = [];
}
