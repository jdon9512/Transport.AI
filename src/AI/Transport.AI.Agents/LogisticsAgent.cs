using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class LogisticsAgent
{
    public Task<RouteCalculatedEvent> Calculate(OrderCreatedEvent order)
    {
        return Task.FromResult(new RouteCalculatedEvent(
            order.OrderId,
            20));
    }
}
