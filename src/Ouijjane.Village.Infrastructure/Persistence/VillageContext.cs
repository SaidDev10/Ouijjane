using Microsoft.EntityFrameworkCore;
using Ouijjane.Village.Domain.Entities;
using System.Reflection;

namespace Ouijjane.Village.Infrastructure.Persistence;
public class VillageContext : DbContext
{
    public VillageContext(DbContextOptions<VillageContext> options) : base(options)
    {
            
    }

    public DbSet<Inhabitant> Inhabitants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
