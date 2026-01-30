using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Service.Application.Orders.CreateOrder;
using Orders.Service.Application.Orders.GetOrder;

namespace Orders.Service.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderHandler _create;
        private readonly GetOrderHandler _get;

        public OrdersController(
            CreateOrderHandler create,
            GetOrderHandler get)
        {
            _create = create;
            _get = get;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var id = await _create.Handle(command);
            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _get.Handle(id);
            return order is null ? NotFound() : Ok(order);
        }
    }

}
