using Transport.AI.Orchestrator.Saga;

namespace Transport.AI.Orchestrator;

public interface ISagaRepository
{
    Task<OrderSagaState> Get(Guid orderId);
    Task Save(OrderSagaState state);
}
