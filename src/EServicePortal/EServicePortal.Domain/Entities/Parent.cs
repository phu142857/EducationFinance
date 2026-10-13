using EServicePortal.Domain.Enums;

namespace EServicePortal.Domain.Entities;

public sealed class Parent
{
    public Guid Id { get; set; }
    public string? ExternalIdentitySubject { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? MailingAddress { get; set; }
    public ParentRegistrationMethod RegistrationMethod { get; set; }
    public ParentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<FamilyMember> FamilyMemberships { get; set; } = [];
    public ICollection<FasApplicationDraft> FasApplicationDrafts { get; set; } = [];
    public ICollection<PaymentRequest> PaymentRequests { get; set; } = [];
}
