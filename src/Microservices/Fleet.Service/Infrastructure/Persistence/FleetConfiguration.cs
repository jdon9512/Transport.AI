using Fleet.Service.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fleet.Service.Infrastructure.Persistence;


public class FleetConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Plate)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(x => x.Plate)
            .IsUnique();

        builder.Property(x => x.CapacityKg)
            .HasPrecision(10, 2);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property<Guid>("_id");

        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}