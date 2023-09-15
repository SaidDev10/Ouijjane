using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ouijjane.Shared.Application.Interfaces.Persistence;
using Ouijjane.Village.Domain.Entities;
using Ouijjane.Village.Infrastructure.Persistence;

namespace Ouijjane.Village.Infrastructure.Seeder;
public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly ILogger<DatabaseSeeder> _logger;
    private readonly VillageContext _context;

    public DatabaseSeeder(ILogger<DatabaseSeeder> logger, VillageContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeDatabaseAsync()
    {
        await MigrateAsync();
        await SeedAsync();
    }

    private async Task MigrateAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initializing the database.");
            throw;
        }
    }

    private async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        //    //// Default roles
        //    //var administratorRole = new IdentityRole(Roles.Administrator);

        //    //if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        //    //{
        //    //    await _roleManager.CreateAsync(administratorRole);
        //    //}

        //    //// Default users
        //    //var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        //    //if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        //    //{
        //    //    await _userManager.CreateAsync(administrator, "Administrator1!");
        //    //    if (!string.IsNullOrWhiteSpace(administratorRole.Name))
        //    //    {
        //    //        await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        //    //    }
        //    //}

        //Default data
        //Seed, if necessary
        if (!_context.Inhabitants.Any())
        {
            _context.Inhabitants.Add(new Inhabitant
            {
                FirstName = "Said",
                LastName = "ID BESSLAM",
                FatherName = "Boujemaa",
                Address = "Marseille, France",
                Phone = "01 23 45 67 89",
                Email = "said@testcom",
                Birthdate = new DateOnly(1991, 07, 25),
                IsMarried = true
            });

            await _context.SaveChangesAsync();
        }
    }
}
