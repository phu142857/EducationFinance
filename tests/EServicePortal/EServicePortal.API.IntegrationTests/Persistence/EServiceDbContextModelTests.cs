using EServicePortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EServicePortal.API.IntegrationTests.Persistence;

public sealed class EServiceDbContextModelTests
{
    [Fact]
    public void Model_ShouldContainOnlyEServiceOwnedTables()
    {
        var options = new DbContextOptionsBuilder<EServiceDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=EServicePortalDb;User Id=sa;Password=TestOnlyPassword123!;TrustServerCertificate=True")
            .Options;

        using var context = new EServiceDbContext(options);
        var tableNames = context.Model.GetEntityTypes()
            .Select(entityType => entityType.GetTableName())
            .Where(tableName => tableName is not null)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var expectedTables = new[]
        {
            "ParentAccounts", "AssistedRegistrations", "ParentProfiles", "FamilyLinks", "ChildProfiles",
            "EducationAccountSummaries", "AccountTransactionViews", "ChargeSummaries",
            "FasApplications", "Payments", "Receipts", "ChatConversations",
            "ParentNotifications", "AuditEvents", "OutboxMessages", "InboxMessages"
        };

        Assert.Equal(expectedTables.Length, tableNames.Count);
        Assert.All(expectedTables, tableName => Assert.Contains(tableName, tableNames));
        Assert.DoesNotContain("StaffUsers", tableNames);
        Assert.DoesNotContain("TopUpRules", tableNames);
        Assert.DoesNotContain("Budgets", tableNames);
    }

    [Fact]
    public void Model_StatusAndTypeProperties_ShouldUseEnums()
    {
        var options = new DbContextOptionsBuilder<EServiceDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=EServicePortalDb;User Id=sa;Password=TestOnlyPassword123!;TrustServerCertificate=True")
            .Options;

        using var context = new EServiceDbContext(options);
        var statusAndTypeProperties = context.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetProperties())
            .Where(property => property.Name.EndsWith("Status", StringComparison.Ordinal)
                || property.Name.EndsWith("Type", StringComparison.Ordinal))
            .ToArray();

        Assert.NotEmpty(statusAndTypeProperties);
        Assert.All(statusAndTypeProperties, property =>
        {
            var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            Assert.True(propertyType.IsEnum, $"{property.DeclaringType.Name}.{property.Name} must be an enum.");
        });
    }
}
