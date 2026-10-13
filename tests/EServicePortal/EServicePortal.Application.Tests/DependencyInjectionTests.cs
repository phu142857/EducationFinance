using EServicePortal.Application;
using Microsoft.Extensions.DependencyInjection;

namespace EServicePortal.Application.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddEServicePortalApplication_returns_the_service_collection()
    {
        var services = new ServiceCollection();

        var result = services.AddEServicePortalApplication();

        Assert.Same(services, result);
    }
}
