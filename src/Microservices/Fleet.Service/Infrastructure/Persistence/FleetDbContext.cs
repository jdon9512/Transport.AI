using Fleet.Service.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Service.Infrastructure.Persistence;

public class FleetDbContext : DbContext
{
    public FleetDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(FleetDbContext).Assembly);
    }
}

