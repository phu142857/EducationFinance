using AdminPortal.Domain.Modules.Audit.Entities;
using AdminPortal.Domain.Modules.Education.Entities;
using AdminPortal.Domain.Modules.FAS.Entities;
using AdminPortal.Domain.Modules.Finance.Entities;
using AdminPortal.Domain.Modules.Identity.Entities;
using AdminPortal.Domain.Modules.Organization.Entities;
using AdminPortal.Infrastructure.Persistence.Messaging;
using Microsoft.EntityFrameworkCore;
using AdminPortal.Application.Abstractions.Persistence;

namespace AdminPortal.Infrastructure.Persistence;

public sealed class AdminDbContext(
    DbContextOptions<AdminDbContext> options)
    : DbContext(options), IAdminDbContext
{
    public DbSet<StaffUser> StaffUsers => Set<StaffUser>();

    public DbSet<School> Schools => Set<School>();
    public DbSet<Cohort> Cohorts => Set<Cohort>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<StudentClass> StudentClasses => Set<StudentClass>();
    public DbSet<FamilyLink> FamilyLinks => Set<FamilyLink>();

    public DbSet<EducationAccount> EducationAccounts => Set<EducationAccount>();
    public DbSet<AccountTransaction> AccountTransactions => Set<AccountTransaction>();
    public DbSet<TopUpRule> TopUpRules => Set<TopUpRule>();

    public DbSet<FeeConfiguration> FeeConfigurations => Set<FeeConfiguration>();
    public DbSet<FeeAssessment> FeeAssessments => Set<FeeAssessment>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Budget> Budgets => Set<Budget>();

    public DbSet<FasScheme> FASSchemes => Set<FasScheme>();
    public DbSet<SubsidyTier> SubsidyTiers => Set<SubsidyTier>();
    public DbSet<FasApplication> FASApplications => Set<FasApplication>();

    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();

    internal DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    internal DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AdminDbContext).Assembly);
        modelBuilder.UseSnakeCaseColumnNames();
    }
}
