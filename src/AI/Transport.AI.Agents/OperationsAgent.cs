using Transport.Shared.Events;

namespace Transport.AI.Agents
{
    public class OperationsAgent
    {
        public Task<TruckReservedEvent> Assign(OrderCreatedEvent order)
        {
            return Task.FromResult(new TruckReservedEvent(
                order.OrderId,
                Guid.NewGuid(),
                Guid.NewGuid()));
        }
    }

}
