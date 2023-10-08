using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Infrastructure.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Options;

namespace Ouijjane.Village.Infrastructure.Extensions;

public static class VillageOptionsExtensions
{
    public static void AddVillageOptions(this IServiceCollection services)
    {
        services.BindValidate<MicroserviceOptions>();
        services.BindValidate<DatabaseOptions>();
    }
}
