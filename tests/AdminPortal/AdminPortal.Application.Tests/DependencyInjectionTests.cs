using AdminPortal.Application;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPortal.Application.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddAdminPortalApplication_returns_the_service_collection()
    {
        var services = new ServiceCollection();

        var result = services.AddAdminPortalApplication();

        Assert.Same(services, result);
    }
}
