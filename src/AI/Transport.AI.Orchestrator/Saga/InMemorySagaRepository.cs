namespace Transport.AI.Orchestrator.Saga;

using System.Collections.Concurrent;

public class InMemorySagaRepository : ISagaRepository
{
    private readonly ConcurrentDictionary<Guid, OrderSagaState> _store = new();

    public Task<OrderSagaState> Get(Guid orderId)
    {
        return Task.FromResult(
            _store.GetOrAdd(orderId, id => new OrderSagaState { OrderId = id })
        );
    }

    public Task Save(OrderSagaState state)
    {
        _store[state.OrderId] = state;
        return Task.CompletedTask;
    }
}
