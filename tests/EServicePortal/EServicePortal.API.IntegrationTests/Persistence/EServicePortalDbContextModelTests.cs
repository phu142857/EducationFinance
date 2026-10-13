using EServicePortal.Domain.Entities;
using EServicePortal.Domain.Enums;
using EServicePortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EServicePortal.API.IntegrationTests.Persistence;

public sealed class EServicePortalDbContextModelTests
{
    private static readonly EServicePortalDbContext Context = CreateContext();

    [Fact]
    public void Model_contains_exactly_the_approved_tables()
    {
        var expectedTables = new[]
        {
            "EducationAccountReadModels", "Families", "FamilyMembers", "FasApplicationDrafts",
            "FasApplicationReadModels", "FeeAssessmentReadModels", "InboxMessages", "OutboxMessages",
            "Parents", "PaymentRequests", "StudentReadModels"
        };

        var actualTables = Context.Model.GetEntityTypes()
            .Select(entity => entity.GetTableName())
            .OrderBy(name => name)
            .ToArray();

        Assert.Equal(expectedTables, actualTables);
        Assert.All(Context.Model.GetEntityTypes(), entity => Assert.Equal("dbo", entity.GetSchema()));
        Assert.Equal(13, Context.Model.GetEntityTypes().SelectMany(entity => entity.GetIndexes()).Count());
    }

    [Fact]
    public void Model_preserves_relationships_and_portal_boundary()
    {
        var foreignKeys = Context.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetForeignKeys())
            .ToArray();

        Assert.Equal(11, foreignKeys.Length);
        Assert.All(foreignKeys, foreignKey =>
        {
            Assert.Equal(DeleteBehavior.NoAction, foreignKey.DeleteBehavior);
            Assert.StartsWith("EServicePortal.Domain", foreignKey.DeclaringEntityType.ClrType.Namespace, StringComparison.Ordinal);
            Assert.StartsWith("EServicePortal.Domain", foreignKey.PrincipalEntityType.ClrType.Namespace, StringComparison.Ordinal);
        });

        var payment = Entity<PaymentRequest>();
        Assert.DoesNotContain(payment.GetIndexes(), index =>
            index.IsUnique && index.Properties.Any(property => property.Name is nameof(PaymentRequest.EducationAccountId) or nameof(PaymentRequest.FeeAssessmentId)));
        Assert.False(payment.FindProperty(nameof(PaymentRequest.EducationAccountId))!.IsNullable);
        Assert.True(payment.FindProperty(nameof(PaymentRequest.FeeAssessmentId))!.IsNullable);
    }

    [Fact]
    public void Model_preserves_filtered_uniqueness_precision_defaults_and_check_constraint()
    {
        var parentIndex = Assert.Single(Entity<Parent>().GetIndexes());
        Assert.True(parentIndex.IsUnique);
        Assert.Equal("[ExternalIdentitySubject] IS NOT NULL", parentIndex.GetFilter());

        var familyIndex = Assert.Single(Entity<Family>().GetIndexes());
        Assert.True(familyIndex.IsUnique);
        Assert.Equal("[FamilyReference] IS NOT NULL", familyIndex.GetFilter());

        var decimalProperties = Context.Model.GetEntityTypes()
            .SelectMany(entity => entity.GetProperties())
            .Where(property => property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
            .ToArray();
        Assert.All(decimalProperties, property => Assert.Equal("decimal(18,2)", property.GetColumnType()));

        Assert.Equal("SGD", Entity<PaymentRequest>().FindProperty(nameof(PaymentRequest.CurrencyCode))!.GetDefaultValue());
        Assert.Equal(0m, Entity<EducationAccountReadModel>().FindProperty(nameof(EducationAccountReadModel.Balance))!.GetDefaultValue());
        var designTimeModel = Context.GetService<IDesignTimeModel>().Model;
        var familyMember = designTimeModel.FindEntityType(typeof(FamilyMember))!;
        Assert.Contains(familyMember.GetCheckConstraints(), constraint => constraint.Name == "CK_FamilyMembers_ExactlyOneMember");
    }

    [Fact]
    public void Model_persists_domain_enums_as_strings()
    {
        AssertStringEnum<Parent, ParentRegistrationMethod>(nameof(Parent.RegistrationMethod));
        AssertStringEnum<Parent, ParentStatus>(nameof(Parent.Status));
        AssertStringEnum<Family, FamilyStatus>(nameof(Family.Status));
        AssertStringEnum<FamilyMember, FamilyRelationshipType>(nameof(FamilyMember.RelationshipType));
        AssertStringEnum<StudentReadModel, EnrollmentStatus>(nameof(StudentReadModel.EnrollmentStatus), isNullable: true);
        AssertStringEnum<EducationAccountReadModel, EducationAccountStatus>(nameof(EducationAccountReadModel.AccountStatus));
        AssertStringEnum<FeeAssessmentReadModel, FeeAssessmentStatus>(nameof(FeeAssessmentReadModel.AssessmentStatus));
        AssertStringEnum<FasApplicationReadModel, FasApplicationStatus>(nameof(FasApplicationReadModel.ApplicationStatus));
        AssertStringEnum<FasApplicationDraft, FasApplicationDraftStatus>(nameof(FasApplicationDraft.DraftStatus));
        AssertStringEnum<PaymentRequest, PaymentMethod>(nameof(PaymentRequest.PaymentMethod));
        AssertStringEnum<PaymentRequest, PaymentRequestStatus>(nameof(PaymentRequest.Status));
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

    private static EServicePortalDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EServicePortalDbContext>()
            .UseSqlServer("Server=localhost;Database=EServicePortalModelTests;Integrated Security=True;TrustServerCertificate=True")
            .Options;

        return new EServicePortalDbContext(options);
    }
}
