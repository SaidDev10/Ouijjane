using Microsoft.EntityFrameworkCore;
using Npgsql;
using Ouijjane.Village.Infrastructure.Persistence;
using Respawn;
using System.Data.Common;
using Testcontainers.PostgreSql;

namespace Ouijjane.Village.Application.Tests.TestDatabases;
public class TestcontainersTestDatabase : ITestDatabase
{
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public TestcontainersTestDatabase()
    {
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new NpgsqlConnection(_connectionString);
        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<VillageContext>()
            .UseNpgsql(_connectionString)
            .Options;

        var context = new VillageContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            TablesToIgnore = ["__EFMigrationsHistory"],
            SchemasToExclude = ["grate"],
            DbAdapter = DbAdapter.Postgres
        });
    }

    public DbConnection GetConnection() => _connection;

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connection);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}