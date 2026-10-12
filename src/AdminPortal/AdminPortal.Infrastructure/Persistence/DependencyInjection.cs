
using AdminPortal.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AdminPortal.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("AdminPortalConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'AdminPortalConnection' is not configured.");
        }

        services.AddDbContext<AdminDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAdminDbContext>(provider =>
            provider.GetRequiredService<AdminDbContext>());

        return services;
    }
}
