namespace EServicePortal.Domain.Modules.Family.Entities;

public sealed class ParentProfile
{
    public Guid ParentId { get; set; }
    public string? ResidentialAddress { get; set; }
    public string? MailingAddress { get; set; }
    public DateTime UpdatedAt { get; set; }
}
