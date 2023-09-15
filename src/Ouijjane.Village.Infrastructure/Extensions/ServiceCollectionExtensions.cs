using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Domain.Enums;
using Ouijjane.Shared.Application.Interfaces.Persistence;
using Ouijjane.Shared.Infrastructure.Extensions;
using Ouijjane.Village.Infrastructure.Persistence;
using Ouijjane.Village.Infrastructure.Seeder;

namespace Ouijjane.Village.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddSharedInfrastructureServices<VillageContext>(DatabaseType.PostgreSql);
        services.AddDatabaseServices(null);
        services.AddDatabaseSeeder();
    }

    private static void AddDatabaseSeeder(this IServiceCollection services) => services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

    private static void AddRepositories(this IServiceCollection services)
    {

    }
}
