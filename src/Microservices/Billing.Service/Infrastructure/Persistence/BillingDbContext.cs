using Billing.Service.Domain.Billing;
using Microsoft.EntityFrameworkCore;

namespace Billing.Service.Infrastructure.Persistence;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<Payment> Payments => Set<Payment>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);
    }
}
