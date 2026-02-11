namespace Transport.AI.Infrastructure.Saga;

using System.Collections.Concurrent;
using Transport.AI.Agents.Saga;

public class InMemorySagaRepository : ISagaRepository
{
    private readonly ConcurrentDictionary<Guid, OrderSagaState> _store = new();

    public Task<OrderSagaState> GetAsync(Guid orderId)
    {
        return Task.FromResult(
            _store.GetOrAdd(orderId, id => new OrderSagaState { OrderId = id })
        );
    }

    public Task SaveAsync(OrderSagaState state)
    {
        _store[state.OrderId] = state;
        return Task.CompletedTask;
    }
}
