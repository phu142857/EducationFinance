using Microsoft.Extensions.DependencyInjection;

namespace AdminPortal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAdminPortalApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
