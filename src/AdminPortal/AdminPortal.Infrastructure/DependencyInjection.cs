using AdminPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAdminPortalInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddDbContext<AdminPortalDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AdminPortalDb")));

        return services;
    }
}
