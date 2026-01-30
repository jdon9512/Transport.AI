using Microsoft.EntityFrameworkCore;
using Orders.Service.Domain.Orders;

namespace Orders.Service.Infrastructure.Persistence;

public class OrdersDbContext : DbContext
{
    public OrdersDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(OrdersDbContext).Assembly);
    }
}
