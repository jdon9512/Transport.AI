using Fleet.Service.Domain.Vehicles;
using Fleet.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Service.Application.Vehicles.GetVehicle;

public class GetVehicleHandler
{
    private readonly FleetDbContext _db;

    public GetVehicleHandler(FleetDbContext db)
    {
        _db = db;
    }

    public async Task<Vehicle?> Handle(Guid id)
    {
        return await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
    }
}

