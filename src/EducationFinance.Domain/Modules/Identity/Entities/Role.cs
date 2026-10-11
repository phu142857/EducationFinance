namespace EducationFinance.Domain.Modules.Identity.Entities;

public class Role
{
    public int RoleId { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}
