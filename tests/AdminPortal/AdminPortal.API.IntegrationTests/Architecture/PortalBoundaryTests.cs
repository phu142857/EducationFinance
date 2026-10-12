using AdminPortal.Infrastructure.Persistence;

namespace AdminPortal.API.IntegrationTests.Architecture;

public sealed class PortalBoundaryTests
{
    [Fact]
    public void Infrastructure_ShouldNotReferenceEServicePortal()
    {
        var references = typeof(AdminDbContext).Assembly.GetReferencedAssemblies();

        Assert.DoesNotContain(
            references,
            reference => reference.Name?.StartsWith("EServicePortal", StringComparison.Ordinal) == true);
    }
}
