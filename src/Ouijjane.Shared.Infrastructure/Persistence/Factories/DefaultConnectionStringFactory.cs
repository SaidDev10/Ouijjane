using Microsoft.Extensions.Configuration;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Npgsql;
using Microsoft.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Settings;

namespace Ouijjane.Shared.Infrastructure.Persistence.Factories;
public class DefaultConnectionStringFactory : IConnectionStringFactory
{
    private readonly IDatabaseNameFactory _databaseNameFactory;
    private readonly DatabaseSettings _databaseSettings;

    public DefaultConnectionStringFactory(IDatabaseNameFactory databaseNameFactory, IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseNameFactory = databaseNameFactory;
        _databaseSettings = databaseSettings.Value;
    }

    public string Create()
    {
        var builder = new NpgsqlConnectionStringBuilder(_databaseSettings.ConnectionString) { Database = _databaseNameFactory.Create() };

        return builder.ConnectionString;
    }
}