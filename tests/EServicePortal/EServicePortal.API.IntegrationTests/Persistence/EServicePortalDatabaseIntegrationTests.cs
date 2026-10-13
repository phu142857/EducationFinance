using EServicePortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EServicePortal.API.IntegrationTests.Persistence;

public sealed class EServicePortalDatabaseIntegrationTests
{
    [DockerSqlServerFact]
    public async Task Development_database_is_reachable_and_fully_migrated()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__EServicePortalDb")!;
        var options = new DbContextOptionsBuilder<EServicePortalDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        await using var context = new EServicePortalDbContext(options);

        Assert.True(await context.Database.CanConnectAsync());
        Assert.Empty(await context.Database.GetPendingMigrationsAsync());
        Assert.Equal(11, await ScalarAsync(context, """
            SELECT COUNT(*)
            FROM sys.tables AS t
            INNER JOIN sys.schemas AS s ON s.schema_id = t.schema_id
            WHERE s.name = N'dbo' AND t.name <> N'__EFMigrationsHistory';
            """));
        Assert.Equal(1, await ScalarAsync(context, """
            SELECT COUNT(*)
            FROM dbo.__EFMigrationsHistory
            WHERE MigrationId = N'20261012165350_InitialCreate'
              AND ProductVersion = N'10.0.12';
            """));
    }

    private static async Task<int> ScalarAsync(DbContext context, string commandText)
    {
        var connection = context.Database.GetDbConnection();
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
        await using var command = connection.CreateCommand();
        command.CommandText = commandText;
        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }
}

internal sealed class DockerSqlServerFactAttribute : FactAttribute
{
    public DockerSqlServerFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ConnectionStrings__EServicePortalDb")))
        {
            Skip = "Set ConnectionStrings__EServicePortalDb to run Docker SQL Server integration tests.";
        }
    }
}
