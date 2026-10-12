using EServicePortal.Domain.Modules.Identity.Enums;

namespace EServicePortal.Domain.Modules.Identity.Entities;

public sealed class AssistedRegistration
{
    public Guid RegistrationId { get; set; }
    public string RegistrationCodeHash { get; set; } = string.Empty;
    public string ParentIdentificationHash { get; set; } = string.Empty;
    public string ChildBirthCertificateHash { get; set; } = string.Empty;
    public AssistedRegistrationStatus Status { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? VerifiedAt { get; set; }
    public Guid? VerifiedByStaffUserId { get; set; }
    public DateTime CreatedAt { get; set; }
}
