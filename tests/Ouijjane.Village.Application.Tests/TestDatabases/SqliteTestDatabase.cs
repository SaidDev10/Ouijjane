using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Ouijjane.Village.Infrastructure.Persistence;
using System.Data.Common;
using System.Data;

namespace Ouijjane.Village.Application.Tests.TestDatabases;
public class SqliteTestDatabase : ITestDatabase
{
    private readonly string _connectionString;
    private readonly SqliteConnection _connection;

    public SqliteTestDatabase()
    {
        _connectionString = "DataSource=:memory:";
        _connection = new SqliteConnection(_connectionString);
    }

    public async Task InitialiseAsync()
    {
        if (_connection.State == ConnectionState.Open)
        {
            await _connection.CloseAsync();
        }

        await _connection.OpenAsync();

        var options = new DbContextOptionsBuilder<VillageContext>()
            .UseSqlite(_connection)
            .Options;

        var context = new VillageContext(options);

        context.Database.Migrate();
    }

    public DbConnection GetConnection() => _connection;

    public async Task ResetAsync()
    {
        await InitialiseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}
