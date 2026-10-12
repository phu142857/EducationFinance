using EServicePortal.Domain.Modules.Identity.Enums;

namespace EServicePortal.Domain.Modules.Identity.Entities;

public sealed class ParentAccount
{
    public Guid ParentId { get; set; }
    public string? SingpassUinFin { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ParentAccountStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
