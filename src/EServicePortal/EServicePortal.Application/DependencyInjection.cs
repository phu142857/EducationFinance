using Microsoft.Extensions.DependencyInjection;

namespace EServicePortal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddEServicePortalApplication(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services;
    }
}
