using System.Reflection;

namespace AdminPortal.Domain.Tests;

public sealed class ArchitectureTests
{
    [Fact]
    public void Domain_does_not_reference_outer_layers_or_the_other_portal()
    {
        var references = Assembly.Load("AdminPortal.Domain")
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .ToArray();

        Assert.DoesNotContain("AdminPortal.Application", references);
        Assert.DoesNotContain("AdminPortal.Infrastructure", references);
        Assert.DoesNotContain("AdminPortal.API", references);
        Assert.DoesNotContain(references, name => name?.StartsWith("EServicePortal", StringComparison.Ordinal) is true);
    }
}
