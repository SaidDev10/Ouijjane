using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Application.Interfaces.Persistence;

namespace Ouijjane.Village.Infrastructure.Extensions;
public static class DatabaseSeederExtensions
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<IDatabaseSeeder>();

        await initialiser.InitializeDatabaseAsync();
    }
}