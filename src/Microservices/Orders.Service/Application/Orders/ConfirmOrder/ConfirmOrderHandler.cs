namespace Orders.Service.Application.Orders.ConfirmOrder;

using global::Orders.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Transport.Shared.Events;

public class ConfirmOrderHandler
{
    private readonly OrdersDbContext _db;

    public ConfirmOrderHandler(OrdersDbContext db)
    {
        _db = db;
    }

    public async Task Handle(ConfirmOrderCommand command)
    {
        var order = await _db.Orders
            .FirstAsync(x => x.Id == command.OrderId);

        order.Confirm();

        await _db.SaveChangesAsync();
    }
}

