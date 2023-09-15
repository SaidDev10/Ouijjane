using Microsoft.Extensions.Configuration;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Npgsql;

namespace Ouijjane.Shared.Infrastructure.Persistence.Factories;
public class DefaultConnectionStringFactory : IConnectionStringFactory
{
    private readonly IDatabaseNameFactory _databaseNameFactory;
    private readonly IConfiguration _configuration;

    public DefaultConnectionStringFactory(IDatabaseNameFactory databaseNameFactory, IConfiguration configuration)
    {
        _databaseNameFactory = databaseNameFactory;
        _configuration = configuration;
    }

    public string Create()
    {
        var builder = new NpgsqlConnectionStringBuilder(_configuration.GetConnectionString("DefaultConnection")) { Database = _databaseNameFactory.Create() };

        return builder.ConnectionString;
    }
}