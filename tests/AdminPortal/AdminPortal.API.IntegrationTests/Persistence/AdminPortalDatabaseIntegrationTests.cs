using AdminPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AdminPortal.API.IntegrationTests.Persistence;

public sealed class AdminPortalDatabaseIntegrationTests
{
    [DockerSqlServerFact]
    public async Task Development_database_is_reachable_and_fully_migrated()
    {
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__AdminPortalDb")!;
        var options = new DbContextOptionsBuilder<AdminPortalDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        await using var context = new AdminPortalDbContext(options);

        Assert.True(await context.Database.CanConnectAsync());
        Assert.Empty(await context.Database.GetPendingMigrationsAsync());
        Assert.Equal(21, await ScalarAsync(context, """
            SELECT COUNT(*)
            FROM sys.tables AS t
            INNER JOIN sys.schemas AS s ON s.schema_id = t.schema_id
            WHERE s.name = N'dbo' AND t.name <> N'__EFMigrationsHistory';
            """));
        Assert.Equal(1, await ScalarAsync(context, """
            SELECT COUNT(*)
            FROM dbo.__EFMigrationsHistory
            WHERE MigrationId = N'20261012165348_InitialCreate'
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
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ConnectionStrings__AdminPortalDb")))
        {
            Skip = "Set ConnectionStrings__AdminPortalDb to run Docker SQL Server integration tests.";
        }
    }
}
