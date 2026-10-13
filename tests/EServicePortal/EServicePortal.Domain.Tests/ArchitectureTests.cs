using System.Reflection;

namespace EServicePortal.Domain.Tests;

public sealed class ArchitectureTests
{
    [Fact]
    public void Domain_does_not_reference_outer_layers_or_the_other_portal()
    {
        var references = Assembly.Load("EServicePortal.Domain")
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .ToArray();

        Assert.DoesNotContain("EServicePortal.Application", references);
        Assert.DoesNotContain("EServicePortal.Infrastructure", references);
        Assert.DoesNotContain("EServicePortal.API", references);
        Assert.DoesNotContain(references, name => name?.StartsWith("AdminPortal", StringComparison.Ordinal) is true);
    }
}
