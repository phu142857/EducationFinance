using AdminPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AdminPortal.Infrastructure.Persistence;

public sealed class AdminPortalDbContext(DbContextOptions<AdminPortalDbContext> options) : DbContext(options)
{
    public DbSet<School> Schools => Set<School>();
    public DbSet<Cohort> Cohorts => Set<Cohort>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<EducationAccount> EducationAccounts => Set<EducationAccount>();
    public DbSet<AccountTransaction> AccountTransactions => Set<AccountTransaction>();
    public DbSet<FeeConfiguration> FeeConfigurations => Set<FeeConfiguration>();
    public DbSet<FeeAssessment> FeeAssessments => Set<FeeAssessment>();
    public DbSet<FeeAssessmentItem> FeeAssessmentItems => Set<FeeAssessmentItem>();
    public DbSet<TopUpRule> TopUpRules => Set<TopUpRule>();
    public DbSet<FasScheme> FasSchemes => Set<FasScheme>();
    public DbSet<FasSchemeDetail> FasSchemeDetails => Set<FasSchemeDetail>();
    public DbSet<FasApplication> FasApplications => Set<FasApplication>();
    public DbSet<FasAssessment> FasAssessments => Set<FasAssessment>();
    public DbSet<FasDecision> FasDecisions => Set<FasDecision>();
    public DbSet<HardcopyFasRecord> HardcopyFasRecords => Set<HardcopyFasRecord>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<BudgetTransaction> BudgetTransactions => Set<BudgetTransaction>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdminPortalDbContext).Assembly);
    }
}
