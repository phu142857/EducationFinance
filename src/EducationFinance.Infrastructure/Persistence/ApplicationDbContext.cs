using EducationFinance.Domain.Modules.Audit.Entities;
using EducationFinance.Domain.Modules.Education.Entities;
using EducationFinance.Domain.Modules.FAS.Entities;
using EducationFinance.Domain.Modules.Finance.Entities;
using EducationFinance.Domain.Modules.Identity.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;
using EducationFinance.Application.Abstractions.Persistence;

namespace EducationFinance.Infrastructure.Persistence;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<RoleAssignment> RoleAssignments => Set<RoleAssignment>();

    public DbSet<School> Schools => Set<School>();
    public DbSet<Cohort> Cohorts => Set<Cohort>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Student> Students => Set<Student>();
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
