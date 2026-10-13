using EServicePortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EServicePortal.Infrastructure.Persistence;

public sealed class EServicePortalDbContext(DbContextOptions<EServicePortalDbContext> options) : DbContext(options)
{
    public DbSet<Parent> Parents => Set<Parent>();
    public DbSet<Family> Families => Set<Family>();
    public DbSet<FamilyMember> FamilyMembers => Set<FamilyMember>();
    public DbSet<StudentReadModel> StudentReadModels => Set<StudentReadModel>();
    public DbSet<EducationAccountReadModel> EducationAccountReadModels => Set<EducationAccountReadModel>();
    public DbSet<FeeAssessmentReadModel> FeeAssessmentReadModels => Set<FeeAssessmentReadModel>();
    public DbSet<FasApplicationReadModel> FasApplicationReadModels => Set<FasApplicationReadModel>();
    public DbSet<FasApplicationDraft> FasApplicationDrafts => Set<FasApplicationDraft>();
    public DbSet<PaymentRequest> PaymentRequests => Set<PaymentRequest>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();
    public DbSet<InboxMessage> InboxMessages => Set<InboxMessage>();

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EServicePortalDbContext).Assembly);
    }
}
