using MassTransit;
using Orders.Service.Application.Orders.ConfirmOrder;
using Transport.Shared.Events;

namespace Orders.Service.Infrastructure.Messaging.Consumers;

public class ConfirmOrderConsumer(ConfirmOrderHandler handler) : IConsumer<ConfirmOrderCommand>
{
    public async Task Consume(ConsumeContext<ConfirmOrderCommand> ctx)
    {
        await handler.Handle(ctx.Message);
    }
}
