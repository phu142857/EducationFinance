using EServicePortal.Application.Abstractions.Persistence;
using EServicePortal.Domain.Modules.Audit.Entities;
using EServicePortal.Domain.Modules.Chatbot.Entities;
using EServicePortal.Domain.Modules.EducationAccounts.Entities;
using EServicePortal.Domain.Modules.Family.Entities;
using EServicePortal.Domain.Modules.FinancialAssistance.Entities;
using EServicePortal.Domain.Modules.Identity.Entities;
using EServicePortal.Domain.Modules.Notifications.Entities;
using EServicePortal.Domain.Modules.Payments.Entities;
using EServicePortal.Infrastructure.Persistence.Messaging;
using Microsoft.EntityFrameworkCore;

namespace EServicePortal.Infrastructure.Persistence;

public sealed class EServiceDbContext(
    DbContextOptions<EServiceDbContext> options)
    : DbContext(options), IEServiceDbContext
{
    public DbSet<ParentAccount> ParentAccounts => Set<ParentAccount>();
    public DbSet<AssistedRegistration> AssistedRegistrations => Set<AssistedRegistration>();
    public DbSet<ParentProfile> ParentProfiles => Set<ParentProfile>();
    public DbSet<FamilyLink> FamilyLinks => Set<FamilyLink>();
    public DbSet<ChildProfile> ChildProfiles => Set<ChildProfile>();
    public DbSet<EducationAccountSummary> EducationAccountSummaries => Set<EducationAccountSummary>();
    public DbSet<AccountTransactionView> AccountTransactionViews => Set<AccountTransactionView>();
    public DbSet<ChargeSummary> ChargeSummaries => Set<ChargeSummary>();
    public DbSet<FasApplication> FasApplications => Set<FasApplication>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Receipt> Receipts => Set<Receipt>();
    public DbSet<ChatConversation> ChatConversations => Set<ChatConversation>();
    public DbSet<ParentNotification> ParentNotifications => Set<ParentNotification>();
    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();

    internal DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    internal DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EServiceDbContext).Assembly);
        modelBuilder.UseSnakeCaseColumnNames();
    }
}
