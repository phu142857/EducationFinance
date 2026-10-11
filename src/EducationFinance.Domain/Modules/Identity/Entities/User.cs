namespace EducationFinance.Domain.Modules.Identity.Entities;

public class User
{
    public Guid UserId { get; set; }
    public string? SingpassUinFin { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string UserType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
