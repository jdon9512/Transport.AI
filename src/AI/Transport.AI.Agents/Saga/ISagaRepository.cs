namespace Transport.AI.Agents.Saga;

public interface ISagaRepository
{
    Task<OrderSagaState> GetAsync(Guid orderId);
    Task SaveAsync(OrderSagaState state);
}
