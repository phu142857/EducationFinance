using AdminPortal.Domain.Entities;
using AdminPortal.Domain.Enums;
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
            "EducationAccounts", "FasApplications", "FasDecisions", "FasSchemes", "FasSchemesDetails",
            "FeeAssessments", "FeeType", "FeeTypeDetail", "HardcopyFasRecords", "InboxMessages",
            "OutboxMessages", "Schools", "StudentEnrollments", "Students", "TopUpRuleDetails", "TopUpRules"
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
        Assert.Equal(27, foreignKeys.Length);
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
        Assert.True(Entity<FasApplication>().FindProperty(nameof(FasApplication.AssessedAmount))!.IsNullable);
        Assert.Equal("Adress", Entity<School>().FindProperty(nameof(School.Address))!.GetColumnName());
        Assert.Equal("nvarchar(255)", Entity<FeeTypeDetail>().FindProperty(nameof(FeeTypeDetail.FeeName))!.GetColumnType());
        Assert.True(Entity<FeeTypeDetail>().FindProperty(nameof(FeeTypeDetail.FeeTypeId))!.IsNullable);
    }

    [Fact]
    public void Model_persists_domain_enums_as_strings()
    {
        AssertStringEnum<School, SchoolStatus>(nameof(School.Status));
        AssertStringEnum<Student, StudentStatus>(nameof(Student.Status), isNullable: true);
        AssertStringEnum<Student, EnrollmentStatus>(nameof(Student.EnrollmentStatus));
        AssertStringEnum<StudentEnrollment, EnrollmentStatus>(nameof(StudentEnrollment.Status), isNullable: true);
        AssertStringEnum<EducationAccount, EducationAccountStatus>(nameof(EducationAccount.Status));
        AssertStringEnum<AccountTransaction, AccountTransactionType>(nameof(AccountTransaction.Type), isNullable: true);
        AssertStringEnum<FeeAssessment, FeeAssessmentStatus>(nameof(FeeAssessment.Status));
        AssertStringEnum<TopUpRule, TopUpRuleStatus>(nameof(TopUpRule.Status));
        AssertStringEnum<FasScheme, FasSchemeType>(nameof(FasScheme.SchemeType));
        AssertStringEnum<FasScheme, FasSchemeStatus>(nameof(FasScheme.Status));
        AssertStringEnum<FasScheme, FasSchemeDividedBy>(nameof(FasScheme.DividedBy));
        AssertStringEnum<FasApplication, FasApplicationStatus>(nameof(FasApplication.Status));
        AssertStringEnum<FasApplication, AssessmentOutcome>(nameof(FasApplication.AssessmentOutcome));
        AssertStringEnum<FasDecision, FasDecisionType>(nameof(FasDecision.Decision));
        AssertStringEnum<HardcopyFasRecord, VerificationStatus>(nameof(HardcopyFasRecord.VerificationStatus));
        AssertStringEnum<Budget, BudgetStatus>(nameof(Budget.Status));
        AssertStringEnum<BudgetTransaction, BudgetMovementType>(nameof(BudgetTransaction.MovementType));
        AssertStringEnum<AdminUser, AdminUserStatus>(nameof(AdminUser.Status));
        AssertStringEnum<OutboxMessage, OutboxMessageStatus>(nameof(OutboxMessage.Status));
        AssertStringEnum<InboxMessage, InboxMessageStatus>(nameof(InboxMessage.Status));
    }

    private static IEntityType Entity<TEntity>() => Context.Model.FindEntityType(typeof(TEntity))!;

    private static void AssertStringEnum<TEntity, TEnum>(string propertyName, bool isNullable = false)
        where TEnum : struct, Enum
    {
        var property = Entity<TEntity>().FindProperty(propertyName)!;
        var expectedClrType = isNullable ? typeof(Nullable<>).MakeGenericType(typeof(TEnum)) : typeof(TEnum);

        Assert.Equal(expectedClrType, property.ClrType);
        Assert.Equal(typeof(string), property.GetTypeMapping().Converter!.ProviderClrType);
    }

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
