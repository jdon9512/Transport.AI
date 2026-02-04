using Orders.Service.Application.Common.Messaging;
using Orders.Service.Domain.Orders;
using Orders.Service.Infrastructure.Persistence;
using Transport.Shared.Events;

namespace Orders.Service.Application.Orders.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly OrdersDbContext _db;
        private readonly IEventBus _bus;

        public CreateOrderHandler(
            OrdersDbContext db,
            IEventBus bus)
        {
            _db = db;
            _bus = bus;
        }

        public async Task<Guid> Handle(CreateOrderCommand command)
        {
            var order = Order.Create(
                command.Origin,
                command.Destination,
                command.CargoDescription,
                command.WeightKg);

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            await _bus.Publish(new OrderCreatedEvent(order.Id));

            return order.Id;
        }
    }

}
