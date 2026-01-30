using Microsoft.EntityFrameworkCore;
using Orders.Service.Domain.Orders;
using Orders.Service.Infrastructure.Persistence;

namespace Orders.Service.Application.Orders.GetOrder
{
    public class GetOrderHandler
    {
        private readonly OrdersDbContext _db;

        public GetOrderHandler(OrdersDbContext db)
        {
            _db = db;
        }

        public async Task<Order?> Handle(Guid id)
        {
            return await _db.Orders.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

}
