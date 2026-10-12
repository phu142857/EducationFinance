using EServicePortal.Infrastructure.Persistence;

namespace EServicePortal.API.IntegrationTests.Architecture;

public sealed class PortalBoundaryTests
{
    [Fact]
    public void Infrastructure_ShouldNotReferenceAdminPortal()
    {
        var references = typeof(EServiceDbContext).Assembly.GetReferencedAssemblies();

        Assert.DoesNotContain(
            references,
            reference => reference.Name?.StartsWith("AdminPortal", StringComparison.Ordinal) == true);
    }
}
