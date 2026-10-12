
using Microsoft.Extensions.DependencyInjection;

namespace EServicePortal.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services;
    }
}
