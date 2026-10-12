using AdminPortal.Domain.Modules.Finance.Entities;
using AdminPortal.Domain.Modules.Identity.Entities;
using AdminPortal.Domain.Modules.Organization.Entities;
using AdminPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using AdminClass = AdminPortal.Domain.Modules.Organization.Entities.Class;

namespace AdminPortal.API.IntegrationTests.Persistence;

public sealed class AdminDbContextModelTests
{
    [Fact]
    public void Model_ShouldContainOnlyAdminOwnedTables()
    {
        var options = new DbContextOptionsBuilder<AdminDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=AdminPortalDb;User Id=sa;Password=TestOnlyPassword123!;TrustServerCertificate=True")
            .Options;

        using var context = new AdminDbContext(options);
        var tableNames = context.Model.GetEntityTypes()
            .Select(entityType => entityType.GetTableName())
            .Where(tableName => tableName is not null)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var expectedTables = new[]
        {
            "StaffUsers", "Schools", "Cohorts", "Classes", "Students", "StudentClasses", "FamilyLinks",
            "EducationAccounts", "AccountTransactions", "TopUpRules",
            "FeeConfigurations", "FeeAssessments", "Payments", "Budgets",
            "FASSchemes", "SubsidyTiers", "FASApplications", "AuditEvents",
            "OutboxMessages", "InboxMessages"
        };

        Assert.Equal(expectedTables.Length, tableNames.Count);
        Assert.All(expectedTables, tableName => Assert.Contains(tableName, tableNames));
        Assert.DoesNotContain("ParentAccounts", tableNames);
        Assert.DoesNotContain("Roles", tableNames);
        Assert.DoesNotContain("Permissions", tableNames);
        Assert.DoesNotContain("RolePermissions", tableNames);
        Assert.DoesNotContain("RoleAssignments", tableNames);
    }

    [Fact]
    public void Model_ShouldFollowRevisedAdminRelationships()
    {
        var options = new DbContextOptionsBuilder<AdminDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=AdminPortalDb;User Id=sa;Password=TestOnlyPassword123!;TrustServerCertificate=True")
            .Options;

        using var context = new AdminDbContext(options);

        var student = context.Model.FindEntityType(typeof(Student))!;
        Assert.Null(student.FindProperty("SchoolId"));
        Assert.Null(student.FindProperty("ClassId"));
        Assert.Empty(student.GetForeignKeys());

        var adminClass = context.Model.FindEntityType(typeof(AdminClass))!;
        Assert.Null(adminClass.FindProperty("SchoolId"));
        Assert.Equal(nameof(AdminClass.CohortId), Assert.Single(adminClass.GetForeignKeys()).Properties.Single().Name);

        var studentClass = context.Model.FindEntityType(typeof(StudentClass))!;
        Assert.Equal(2, studentClass.FindPrimaryKey()!.Properties.Count);
        Assert.Equal(2, studentClass.GetForeignKeys().Count());

        var assessment = context.Model.FindEntityType(typeof(FeeAssessment))!;
        Assert.Null(assessment.FindProperty("StudentId"));
        Assert.Null(assessment.FindProperty("Term"));
        Assert.Equal(nameof(FeeAssessment.AccountId), Assert.Single(assessment.GetForeignKeys()).Properties.Single().Name);

        var feeConfiguration = context.Model.FindEntityType(typeof(FeeConfiguration))!;
        Assert.Null(feeConfiguration.FindProperty("SchoolId"));
        Assert.Equal(typeof(DateOnly), feeConfiguration.FindProperty(nameof(FeeConfiguration.PaymentDeadline))!.ClrType);

        var staffUser = context.Model.FindEntityType(typeof(StaffUser))!;
        Assert.Null(staffUser.FindProperty("StaffType"));
        Assert.NotNull(staffUser.FindProperty(nameof(StaffUser.PasswordHash)));
        Assert.NotNull(staffUser.FindProperty(nameof(StaffUser.Address)));
    }

    [Fact]
    public void Model_StatusAndTypeProperties_ShouldUseEnums()
    {
        var options = new DbContextOptionsBuilder<AdminDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=AdminPortalDb;User Id=sa;Password=TestOnlyPassword123!;TrustServerCertificate=True")
            .Options;

        using var context = new AdminDbContext(options);
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
