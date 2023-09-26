using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ouijjane.Village.Infrastructure.Persistence;
using System.Data.Common;

namespace Ouijjane.Village.Application.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbConnection _connection;

    public CustomWebApplicationFactory(DbConnection connection)
    {
        _connection = connection;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            //services
            //    .RemoveAll<IUser>()
            //    .AddTransient(provider => Mock.Of<IUser>(s => s.Id == GetUserId()));

            services
                .RemoveAll<DbContextOptions<VillageContext>>()
                .AddDbContext<VillageContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
#if (UseSQLite)
                    options.UseSqlite(_connection);
#else
                    options.UseNpgsql(_connection);
#endif
                });
        });
    }
}
