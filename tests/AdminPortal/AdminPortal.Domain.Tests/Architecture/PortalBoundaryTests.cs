using AdminPortal.Domain.Common;

namespace AdminPortal.Domain.Tests.Architecture;

public sealed class PortalBoundaryTests
{
    [Fact]
    public void Domain_ShouldNotReferenceEServicePortal()
    {
        var references = typeof(Entity).Assembly.GetReferencedAssemblies();

        Assert.DoesNotContain(
            references,
            reference => reference.Name?.StartsWith("EServicePortal", StringComparison.Ordinal) == true);
    }
}
