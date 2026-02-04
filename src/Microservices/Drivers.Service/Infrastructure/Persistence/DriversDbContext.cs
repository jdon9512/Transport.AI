using Drivers.Service.Domain.Drivers;
using Microsoft.EntityFrameworkCore;

namespace Drivers.Service.Infrastructure.Persistence;

public class DriversDbContext : DbContext
{
    public DriversDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Driver> Drivers => Set<Driver>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DriversDbContext).Assembly);
    }
}
