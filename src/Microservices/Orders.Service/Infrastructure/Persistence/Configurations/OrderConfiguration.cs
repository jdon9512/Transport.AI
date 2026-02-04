using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Service.Domain.Orders;

namespace Orders.Service.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        // PK
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        // Origin
        builder.Property(x => x.Origin)
            .IsRequired()
            .HasMaxLength(200);

        // Destination
        builder.Property(x => x.Destination)
            .IsRequired()
            .HasMaxLength(200);

        // Cargo
        builder.Property(x => x.CargoDescription)
            .HasMaxLength(300);

        // Weight
        builder.Property(x => x.WeightKg)
            .HasPrecision(10, 2)
            .IsRequired();

        // Status enum → string
        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(50);

        // CreatedAt
        builder.Property(x => x.CreatedAt)
            .IsRequired();

        // Indexes útiles
        builder.HasIndex(x => x.Status);
        builder.HasIndex(x => x.CreatedAt);

        // Optional: composite index para búsquedas comunes
        builder.HasIndex(x => new { x.Origin, x.Destination });
    }
}

