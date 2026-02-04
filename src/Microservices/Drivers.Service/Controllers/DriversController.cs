using Drivers.Service.Application.Drivers.CreateDriver;
using Drivers.Service.Application.Drivers.GetDriver;
using Microsoft.AspNetCore.Mvc;

namespace Drivers.Service.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController : ControllerBase
{
    private readonly CreateDriverHandler _create;
    private readonly GetDriverHandler _get;

    public DriversController(
        CreateDriverHandler create,
        GetDriverHandler get)
    {
        _create = create;
        _get = get;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDriverCommand command)
    {
        var id = await _create.Handle(command);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var driver = await _get.Handle(id);
        return driver == null ? NotFound() : Ok(driver);
    }
}
