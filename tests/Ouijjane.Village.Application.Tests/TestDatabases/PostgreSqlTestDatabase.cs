using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ouijjane.Village.Infrastructure.Persistence;
using Respawn;
using System.Data.Common;

namespace Ouijjane.Village.Application.Tests.TestDatabases;
public class PostgreSqlTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private SqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public PostgreSqlTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        _connectionString = connectionString;
    }
    public async Task InitialiseAsync()
    {
        _connection = new SqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<VillageContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new VillageContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"],
            SchemasToExclude = ["grate"]
        });
    }

    public DbConnection GetConnection() => _connection;

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }
    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
