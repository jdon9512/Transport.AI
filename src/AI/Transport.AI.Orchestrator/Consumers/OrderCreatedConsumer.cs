namespace Transport.AI.Orchestrator.Consumers;

using MassTransit;
using Transport.Shared.Events;
using Transport.AI.Orchestrator.Orchestration;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly OrchestratorCoordinator _coordinator;

    public OrderCreatedConsumer(OrchestratorCoordinator coordinator)
    {
        _coordinator = coordinator;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> ctx)
    {
        await _coordinator.Handle(ctx.Message);
    }
}
