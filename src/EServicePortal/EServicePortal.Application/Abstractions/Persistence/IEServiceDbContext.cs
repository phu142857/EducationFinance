using EServicePortal.Domain.Modules.Audit.Entities;
using EServicePortal.Domain.Modules.Chatbot.Entities;
using EServicePortal.Domain.Modules.EducationAccounts.Entities;
using EServicePortal.Domain.Modules.Family.Entities;
using EServicePortal.Domain.Modules.FinancialAssistance.Entities;
using EServicePortal.Domain.Modules.Identity.Entities;
using EServicePortal.Domain.Modules.Notifications.Entities;
using EServicePortal.Domain.Modules.Payments.Entities;
using Microsoft.EntityFrameworkCore;

namespace EServicePortal.Application.Abstractions.Persistence;

public interface IEServiceDbContext
{
    DbSet<ParentAccount> ParentAccounts { get; }
    DbSet<AssistedRegistration> AssistedRegistrations { get; }
    DbSet<ParentProfile> ParentProfiles { get; }
    DbSet<FamilyLink> FamilyLinks { get; }
    DbSet<ChildProfile> ChildProfiles { get; }
    DbSet<EducationAccountSummary> EducationAccountSummaries { get; }
    DbSet<AccountTransactionView> AccountTransactionViews { get; }
    DbSet<ChargeSummary> ChargeSummaries { get; }
    DbSet<FasApplication> FasApplications { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Receipt> Receipts { get; }
    DbSet<ChatConversation> ChatConversations { get; }
    DbSet<ParentNotification> ParentNotifications { get; }
    DbSet<AuditEvent> AuditEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
