using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Service.Domain.Orders;

namespace Orders.Service.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Origin).IsRequired();
            builder.Property(x => x.Destination).IsRequired();
            builder.Property(x => x.Status).HasConversion<string>();
        }
    }

} 