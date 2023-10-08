using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Interfaces.Persistence.Factories;
using Ouijjane.Shared.Application.Interfaces.Persistence.Repositories;
using Ouijjane.Shared.Infrastructure.Constants;
using Ouijjane.Shared.Infrastructure.Extensions.Options;
using Ouijjane.Shared.Infrastructure.Interceptors;
using Ouijjane.Shared.Infrastructure.Persistence.Factories;
using Ouijjane.Shared.Infrastructure.Persistence.Repositories;
using Ouijjane.Shared.Infrastructure.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Ouijjane.Shared.Infrastructure.Extensions.Database;
public static class DatabaseExtensions
{
    public static void AddDatabase<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
    {
        services.AddDatabaseServices(); //If custom factory should be passed, this method should called in service layer
        services.AddInterceptors();
        services.AddDbContext<TContext>(configuration);
        services.AddOutboxServices<TContext>();
    }

    public static void AddDatabaseServices(this IServiceCollection services, Action<IServiceCollection>? useCustomFactory = null)
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

    private static void AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : DbContext
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var connectionString = sp.GetRequiredService<IConnectionStringFactory>().Create();
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            var databaseOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
            options.UseDatabase(databaseOptions!.DBProvider, connectionString);
        });

        services.AddTransient<IUnitOfWork, UnitOfWork<TContext>>();
    }

    private static void AddOutboxServices<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        //services.AddScoped<IOutboxStore, OutboxStore>();
        //services.AddScoped<OutboxEventsPublisher>();

        //services.AddScoped<IOutboxDbContext>(provider => provider.GetRequiredService<TContext>());
    }
    
    private static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder builder, string dbProvider, string connectionString)
    {
        return dbProvider.ToLowerInvariant() switch
        {
            DbProviderKeys.Npgsql => builder.UseNpgsql(connectionString),

            //TODO: when implementing multi-tenancy
            //DbProviderKeys.Npgsql => builder.UseNpgsql(connectionString, e =>
            //                     e.MigrationsAssembly("Migrators.PostgreSQL")),
            //DbProviderKeys.SqlServer => builder.UseSqlServer(connectionString, e =>
            //                     e.MigrationsAssembly("Migrators.MSSQL")),
            //DbProviderKeys.MySql => builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), e =>
            //                     e.MigrationsAssembly("Migrators.MySQL")
            //                      .SchemaBehavior(MySqlSchemaBehavior.Ignore)),
            //DbProviderKeys.Oracle => builder.UseOracle(connectionString, e =>
            //                     e.MigrationsAssembly("Migrators.Oracle")),
            //DbProviderKeys.SqLite => builder.UseSqlite(connectionString, e =>
            //                     e.MigrationsAssembly("Migrators.SqLite")),
            _ => throw new InvalidOperationException($"DB Provider {dbProvider} is not supported."), //TODO: localization
        };
    }
}
