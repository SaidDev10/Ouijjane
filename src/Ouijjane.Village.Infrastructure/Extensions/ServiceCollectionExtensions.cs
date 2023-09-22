using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Interfaces.Persistence;
using Ouijjane.Shared.Infrastructure.Extensions;
using Ouijjane.Village.Infrastructure.Persistence;
using Ouijjane.Village.Infrastructure.Seeder;
using Microsoft.Extensions.Configuration;

namespace Ouijjane.Village.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedInfrastructureServices<VillageContext>(configuration);
        services.AddDatabaseServices(null);
        services.AddDatabaseSeeder();
    }

    private static void AddDatabaseSeeder(this IServiceCollection services) => services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

}
