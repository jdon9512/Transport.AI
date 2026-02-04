using Drivers.Service.Domain.Drivers;
using Drivers.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Drivers.Service.Application.Drivers.GetDriver;

public class GetDriverHandler
{
    private readonly DriversDbContext _db;

    public GetDriverHandler(DriversDbContext db)
    {
        _db = db;
    }

    public async Task<Driver?> Handle(Guid id)
    {
        return await _db.Drivers.FirstOrDefaultAsync(x => x.Id == id);
    }
}
