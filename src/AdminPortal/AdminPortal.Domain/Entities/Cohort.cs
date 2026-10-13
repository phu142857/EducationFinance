namespace AdminPortal.Domain.Entities;

public sealed class Cohort
{
    public Guid Id { get; set; }
    public Guid SchoolId { get; set; }
    public int StartYear { get; set; }
    public int EndYear { get; set; }
    public string LevelName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public School School { get; set; } = null!;
    public ICollection<Class> Classes { get; set; } = [];
}
