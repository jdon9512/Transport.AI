using Transport.Shared.Events;

namespace Transport.AI.Agents;

public class FinanceAgent
{
    public Task<CostCalculatedEvent> Calculate(OrderCreatedEvent order, double km)
    {
        return Task.FromResult(new CostCalculatedEvent(
            order.OrderId,
            (decimal)km * 5));
    }
}
