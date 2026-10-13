using EServicePortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EServicePortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddEServicePortalInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddDbContext<EServicePortalDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("EServicePortalDb")));

        return services;
    }
}
