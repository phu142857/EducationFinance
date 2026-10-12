
using EducationFinance.Domain.Modules.Audit.Entities;
using EducationFinance.Domain.Modules.Education.Entities;
using EducationFinance.Domain.Modules.FAS.Entities;
using EducationFinance.Domain.Modules.Finance.Entities;
using EducationFinance.Domain.Modules.Identity.Entities;
using EducationFinance.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;

namespace EducationFinance.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<Permission> Permissions { get; }
    DbSet<RolePermission> RolePermissions { get; }
    DbSet<RoleAssignment> RoleAssignments { get; }

    DbSet<School> Schools { get; }
    DbSet<Cohort> Cohorts { get; }
    DbSet<Class> Classes { get; }
    DbSet<Student> Students { get; }
    DbSet<FamilyLink> FamilyLinks { get; }

    DbSet<EducationAccount> EducationAccounts { get; }
    DbSet<AccountTransaction> AccountTransactions { get; }
    DbSet<TopUpRule> TopUpRules { get; }

    DbSet<FeeConfiguration> FeeConfigurations { get; }
    DbSet<FeeAssessment> FeeAssessments { get; }
    DbSet<Payment> Payments { get; }
    DbSet<Budget> Budgets { get; }

    DbSet<FasScheme> FASSchemes { get; }
    DbSet<SubsidyTier> SubsidyTiers { get; }
    DbSet<FasApplication> FASApplications { get; }

    DbSet<AuditEvent> AuditEvents { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}
