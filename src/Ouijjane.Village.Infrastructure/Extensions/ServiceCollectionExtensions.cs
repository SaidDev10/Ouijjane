using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Interfaces.Persistence;
using Ouijjane.Shared.Infrastructure.Extensions;
using Ouijjane.Village.Infrastructure.Persistence;
using Ouijjane.Village.Infrastructure.Seeder;
using Microsoft.Extensions.Configuration;

namespace Ouijjane.Village.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddVillageInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddVillageOptions();

        services.AddInfrastructureServices<VillageContext>(configuration);

        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>();

        return services;
    }
}
