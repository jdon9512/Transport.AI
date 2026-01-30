using Orders.Service.Domain.Orders;
using Orders.Service.Infrastructure.Persistence;

namespace Orders.Service.Application.Orders.CreateOrder
{
    public class CreateOrderHandler
    {
        private readonly OrdersDbContext _db;

        public CreateOrderHandler(OrdersDbContext db)
        {
            _db = db;
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

            return order.Id;
        }
    }

}
