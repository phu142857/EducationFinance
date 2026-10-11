namespace EducationFinance.Domain.Modules.Identity.Entities;

public class Permission
{
    public int PermissionId { get; set; }
    public string PermissionCode { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty;
    public string? Description { get; set; }
}
