using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Infrastructure.Extensions;
using Ouijjane.Shared.Infrastructure.Persistence.Factories;
using Ouijjane.Shared.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ouijjane.Shared.Infrastructure.Interceptors;
using Microsoft.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Ouijjane.Shared.Infrastructure.Extensions;
public static class ServiceCollectionExtensions
{
    //private static readonly ILogger _logger = Log.ForContext(typeof(Startup)); //TODO
    public static void AddSharedInfrastructureServices<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
    {
        services.AddDbContext<TContext>();
        services.AddOutboxServices<TContext>();
        services.AddInterceptors();
        services.AddSettings(configuration);
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

    private static void AddInterceptors(this IServiceCollection services)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    }

    private static void AddDbContext<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IConnectionStringFactory>().Create();
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            var databaseSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            options.UseDatabase(databaseSettings.DBProvider, connectionString);
        });

        services.AddTransient<IUnitOfWork, UnitOfWork<TContext>>();
    }

    private static void AddOutboxServices<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        //services.AddScoped<IOutboxStore, OutboxStore>();
        //services.AddScoped<OutboxEventsPublisher>();

        //services.AddScoped<IOutboxDbContext>(provider => provider.GetRequiredService<TContext>());
    }


    private static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MicroServiceSettings>(configuration.GetSection(nameof(MicroServiceSettings)));

        services.AddOptions<DatabaseSettings>()
                .BindConfiguration(nameof(DatabaseSettings))
                //.PostConfigure(databaseSettings =>
                //{
                //    _logger.Information("Current DB Provider: {dbProvider}", databaseSettings.DBProvider);//TODO
                //})
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }
}
