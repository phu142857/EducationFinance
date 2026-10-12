
using AdminPortal.Domain.Modules.Audit.Entities;
using AdminPortal.Domain.Modules.Education.Entities;
using AdminPortal.Domain.Modules.FAS.Entities;
using AdminPortal.Domain.Modules.Finance.Entities;
using AdminPortal.Domain.Modules.Identity.Entities;
using AdminPortal.Domain.Modules.Organization.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.Application.Abstractions.Persistence;

public interface IAdminDbContext
{
    DbSet<StaffUser> StaffUsers { get; }

    DbSet<School> Schools { get; }
    DbSet<Cohort> Cohorts { get; }
    DbSet<Class> Classes { get; }
    DbSet<Student> Students { get; }
    DbSet<StudentClass> StudentClasses { get; }
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
