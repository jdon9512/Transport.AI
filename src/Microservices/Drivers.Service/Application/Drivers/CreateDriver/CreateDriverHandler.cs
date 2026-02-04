using Drivers.Service.Domain.Drivers;
using Drivers.Service.Infrastructure.Persistence;

namespace Drivers.Service.Application.Drivers.CreateDriver;

public class CreateDriverHandler
{
    private readonly DriversDbContext _db;

    public CreateDriverHandler(DriversDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateDriverCommand command)
    {
        var driver = Driver.Create(command.FullName);

        _db.Drivers.Add(driver);
        await _db.SaveChangesAsync();

        return driver.Id;
    }
}
