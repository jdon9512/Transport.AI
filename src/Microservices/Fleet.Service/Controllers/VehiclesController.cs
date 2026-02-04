using Fleet.Service.Application.Vehicles.CreateVehicle;
using Fleet.Service.Application.Vehicles.GetVehicle;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Service.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly CreateVehicleHandler _create;
    private readonly GetVehicleHandler _get;

    public VehiclesController(CreateVehicleHandler create, GetVehicleHandler get)
    {
        _create = create;
        _get = get;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVehicleCommand command)
    {
        return Ok(await _create.Handle(command));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var vehicle = await _get.Handle(id);
        return vehicle == null ? NotFound() : Ok(vehicle);
    }
}

