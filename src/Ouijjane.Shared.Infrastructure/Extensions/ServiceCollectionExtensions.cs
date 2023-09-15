using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Domain.Enums;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Infrastructure.Extensions;
using Ouijjane.Shared.Infrastructure.Persistence.Factories;
using Ouijjane.Shared.Infrastructure.Persistence.Repositories;

namespace Ouijjane.Shared.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddSharedInfrastructureServices<TContext>(this IServiceCollection services, DatabaseType databaseType) where TContext : DbContext
    {
        services.AddDbContext<TContext>(databaseType);
        services.AddOutboxServices<TContext>();
    }

    public static void AddDatabaseServices(this IServiceCollection services, Action<IServiceCollection>? useCustomFactory)
    {
        if (useCustomFactory == null)
        {
            services.AddSingleton<IDatabaseNameFactory, DefaultDatabaseNameFactory>();
            services.AddScoped<IConnectionStringFactory, DefaultConnectionStringFactory>();
        }
        else
        {
            useCustomFactory(services);
        }
    }

    private static void AddDbContext<TContext>(this IServiceCollection services, DatabaseType databaseType) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IConnectionStringFactory>().Create();
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            switch (databaseType)
            {
                case DatabaseType.PostgreSql:
                    options.UseNpgsql(connectionString);
                    break;

                case DatabaseType.SqlServer:
                default:
                    throw new NotImplementedException();
            }
        });

        services.AddTransient<IUnitOfWork, UnitOfWork<TContext>>();
    }

    private static void AddOutboxServices<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        //services.AddScoped<IOutboxStore, OutboxStore>();
        //services.AddScoped<OutboxEventsPublisher>();

        //services.AddScoped<IOutboxDbContext>(provider => provider.GetRequiredService<TContext>());
    }
}
