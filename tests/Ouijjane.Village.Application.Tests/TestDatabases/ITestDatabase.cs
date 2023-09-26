using System.Data.Common;

namespace Ouijjane.Village.Application.Tests.TestDatabases;
public interface ITestDatabase
{
    Task InitialiseAsync();

    DbConnection GetConnection();

    Task ResetAsync();

    Task DisposeAsync();
}
