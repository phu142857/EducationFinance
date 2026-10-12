
using EServicePortal.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EServicePortal.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("EServicePortalConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'EServicePortalConnection' is not configured.");
        }

        services.AddDbContext<EServiceDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IEServiceDbContext>(provider =>
            provider.GetRequiredService<EServiceDbContext>());

        return services;
    }
}
