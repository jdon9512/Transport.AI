using Fleet.Service.Domain.Vehicles;
using Fleet.Service.Infrastructure.Persistence;

namespace Fleet.Service.Application.Vehicles.CreateVehicle;

public class CreateVehicleHandler
{
    private readonly FleetDbContext _db;

    public CreateVehicleHandler(FleetDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateVehicleCommand command)
    {
        var vehicle = Vehicle.Create(command.Plate, command.CapacityKg);

        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();

        return vehicle.Id;
    }
}

