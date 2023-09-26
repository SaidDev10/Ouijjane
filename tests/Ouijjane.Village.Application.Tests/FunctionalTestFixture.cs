using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ouijjane.Shared.Domain.Entities;
using Ouijjane.Village.Application.Tests.TestDatabases;
using Ouijjane.Village.Infrastructure.Persistence;

namespace Ouijjane.Village.Application.Tests;

[CollectionDefinition("Functional test collection")]
public class FunctionalTestCollection : ICollectionFixture<FunctionalTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

public class FunctionalTestFixture : IAsyncLifetime
{
    private static ITestDatabase _database;
    private static CustomWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    //private static string? _userId;

    public async Task InitializeAsync()
    {
        _database = await TestDatabaseFactory.CreateAsync();

        _factory = new CustomWebApplicationFactory(_database.GetConnection());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    //public static string? GetUserId()
    //{
    //    return _userId;
    //}

    //public static async Task<string> RunAsDefaultUserAsync()
    //{
    //    return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    //}

    //public static async Task<string> RunAsAdministratorAsync()
    //{
    //    return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { Roles.Administrator });
    //}

    //public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    //{
    //    using var scope = _scopeFactory.CreateScope();

    //    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    //    var user = new ApplicationUser { UserName = userName, Email = userName };

    //    var result = await userManager.CreateAsync(user, password);

    //    if (roles.Any())
    //    {
    //        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    //        foreach (var role in roles)
    //        {
    //            await roleManager.CreateAsync(new IdentityRole(role));
    //        }

    //        await userManager.AddToRolesAsync(user, roles);
    //    }

    //    if (result.Succeeded)
    //    {
    //        _userId = user.Id;

    //        return _userId;
    //    }

    //    var errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

    //    throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    //}

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception)
        {
        }

        //_userId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : Entity
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<VillageContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<VillageContext>();

        await context.AddRangeAsync(entities);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : Entity
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<VillageContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    public async Task DisposeAsync()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}