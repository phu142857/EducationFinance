using EServicePortal.Domain.Common;

namespace EServicePortal.Domain.Tests.Architecture;

public sealed class PortalBoundaryTests
{
    [Fact]
    public void Domain_ShouldNotReferenceAdminPortal()
    {
        var references = typeof(Entity).Assembly.GetReferencedAssemblies();

        Assert.DoesNotContain(
            references,
            reference => reference.Name?.StartsWith("AdminPortal", StringComparison.Ordinal) == true);
    }
}
