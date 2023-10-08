using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Ouijjane.Shared.Infrastructure.Extensions.Database;

namespace Ouijjane.Shared.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
    {
        services.AddDatabase<TContext>(configuration);
    }
}
