using EducationFinance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EducationFinance.API.IntegrationTests.Persistence;

public sealed class ApplicationDbContextModelTests
{
    [Fact]
    public void Model_ShouldContainAllExpectedTables()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(
                "Server=localhost,1433;Database=EducationFinanceDb;" +
                "User Id=sa;Password=TestOnlyPassword123!;" +
                "TrustServerCertificate=True")
            .Options;

        using var context = new ApplicationDbContext(options);

        var entityTypes = context.Model.GetEntityTypes();

        var tableNames = entityTypes
            .Select(entityType => entityType.GetTableName())
            .Where(tableName => tableName is not null)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var expectedTables = new[]
        {
            "Users",
            "Roles",
            "Permissions",
            "RolePermissions",
            "RoleAssignments",
            "Schools",
            "Cohorts",
            "Classes",
            "Students",
            "FamilyLinks",
            "EducationAccounts",
            "AccountTransactions",
            "TopUpRules",
            "FeeConfigurations",
            "FeeAssessments",
            "FASSchemes",
            "SubsidyTiers",
            "FASApplications",
            "Payments",
            "Budgets",
            "AuditEvents"
        };

        Assert.Equal(expectedTables.Length, expectedTables.Distinct().Count());

        foreach (var tableName in expectedTables)
        {
            Assert.Contains(tableName, tableNames);
        }

        Assert.Equal(expectedTables.Length, tableNames.Count);
    }
}
