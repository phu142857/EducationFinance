using AdminPortal.Domain.Entities;
using AdminPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdminPortal.API.IntegrationTests.Persistence;

public sealed class AdminPortalDbContextModelTests
{
    private static readonly AdminPortalDbContext Context = CreateContext();

    [Fact]
    public void Model_contains_exactly_the_approved_tables()
    {
        var expectedTables = new[]
        {
            "AccountTransactions", "AdminUsers", "Budgets", "BudgetTransactions", "Classes", "Cohorts",
            "EducationAccounts", "FasApplications", "FasAssessments", "FasDecisions", "FasSchemes",
            "FasSchemesDetails", "FeeAssessmentItems", "FeeAssessments", "FeeConfigurations",
            "HardcopyFasRecords", "InboxMessages", "OutboxMessages", "Schools", "Students", "TopUpRules"
        };

        var actualTables = Context.Model.GetEntityTypes()
            .Select(entity => entity.GetTableName())
            .OrderBy(name => name)
            .ToArray();

        Assert.Equal(expectedTables, actualTables);
        Assert.All(Context.Model.GetEntityTypes(), entity => Assert.Equal("dbo", entity.GetSchema()));
        Assert.Equal(17, Context.Model.GetEntityTypes().SelectMany(entity => entity.GetIndexes()).Count());
    }

    [Fact]
    public void Model_preserves_keys_relationships_and_portal_boundary()
    {
        var entities = Context.Model.GetEntityTypes().ToArray();
        Assert.All(entities, entity => Assert.Single(entity.FindPrimaryKey()!.Properties));

        var foreignKeys = entities.SelectMany(entity => entity.GetForeignKeys()).ToArray();
        Assert.Equal(25, foreignKeys.Length);
        Assert.All(foreignKeys, foreignKey =>
        {
            Assert.Equal(DeleteBehavior.NoAction, foreignKey.DeleteBehavior);
            Assert.StartsWith("AdminPortal.Domain", foreignKey.DeclaringEntityType.ClrType.Namespace, StringComparison.Ordinal);
            Assert.StartsWith("AdminPortal.Domain", foreignKey.PrincipalEntityType.ClrType.Namespace, StringComparison.Ordinal);
        });
    }

    [Fact]
    public void Model_preserves_financial_precision_unique_indexes_and_nullability()
    {
        var decimalProperties = Context.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetProperties())
            .Where(property => property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
            .ToArray();

        Assert.NotEmpty(decimalProperties);
        Assert.All(decimalProperties, property => Assert.Equal("decimal(18,2)", property.GetColumnType()));
        Assert.True(Entity<EducationAccount>().GetIndexes().Single(index => PropertyNames(index) == "StudentId").IsUnique);
        Assert.True(Entity<AccountTransaction>().GetIndexes().Single(index => PropertyNames(index) == "BusinessReference").IsUnique);
        Assert.True(Entity<BudgetTransaction>().GetIndexes().Single(index => PropertyNames(index) == "BusinessReference").IsUnique);
        Assert.True(Entity<Student>().FindProperty(nameof(Student.CurrentClassId))!.IsNullable);
        Assert.True(Entity<FasAssessment>().FindProperty(nameof(FasAssessment.AssessedAmount))!.IsNullable);
        Assert.Equal("Adress", Entity<School>().FindProperty(nameof(School.Address))!.GetColumnName());
        Assert.Equal("nvarchar(100)", Entity<FeeAssessmentItem>().FindProperty(nameof(FeeAssessmentItem.Name))!.GetColumnType());
    }

    private static IEntityType Entity<TEntity>() => Context.Model.FindEntityType(typeof(TEntity))!;

    private static string PropertyNames(IReadOnlyIndex index) =>
        string.Join(",", index.Properties.Select(property => property.Name));

    private static AdminPortalDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AdminPortalDbContext>()
            .UseSqlServer("Server=localhost;Database=AdminPortalModelTests;Integrated Security=True;TrustServerCertificate=True")
            .Options;

        return new AdminPortalDbContext(options);
    }
}
