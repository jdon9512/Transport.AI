using MassTransit;
using MassTransit.Saga;
using Transport.AI.Orchestrator.Saga;
using Transport.Shared.Events;
using Transport.AI.Agents;

namespace Transport.AI.Orchestrator.Orchestration;

public class OrchestratorCoordinator
{
    private readonly ISagaRepository _repo;
    private readonly LogisticsAgent _logistics;
    private readonly OperationsAgent _operations;
    private readonly FinanceAgent _finance;
    private readonly IPublishEndpoint _bus;

    public OrchestratorCoordinator(
        ISagaRepository repo,
        LogisticsAgent logistics,
        OperationsAgent operations,
        FinanceAgent finance,
        IPublishEndpoint bus)
    {
        _repo = repo;
        _logistics = logistics;
        _operations = operations;
        _finance = finance;
        _bus = bus;
    }

    public async Task Handle(OrderCreatedEvent evt)
    {
        var saga = new OrderSagaState { OrderId = evt.OrderId };

        saga.Logistics = await _logistics.CalculateRoute(saga);
        saga.Operations = await _operations.AssignTruck(saga);
        saga.Finance = await _finance.CalculateCost(saga);

        Validate(saga);

        await _repository.SaveAsync(saga);

        if (!saga.Approved)
        {
            await _publisher.Publish(new RejectOrderCommand
            {
                OrderId = saga.OrderId,
                Reasons = saga.RejectionReasons
            });

            return;
        }

        await _publisher.Publish(new ConfirmOrderCommand
        {
            OrderId = saga.OrderId,
            Route = saga.Logistics.Data["route"],
            Truck = saga.Operations.Data["truck"],
            Cost = saga.Finance.Data["cost"]
        });
    }

    private void Validate(OrderSagaState saga)
    {
        if (saga.Logistics == null || !saga.Logistics.Success)
            saga.RejectionReasons.Add("Invalid logistics decision");

        if (saga.Operations == null || !saga.Operations.Success)
            saga.RejectionReasons.Add("No vehicle available");

        if (saga.Finance == null || !saga.Finance.Success)
            saga.RejectionReasons.Add("Cost calculation failed");

        // Ejemplo de regla cruzada
        if (saga.Finance?.Data.TryGetValue("cost", out var costStr) == true
            && decimal.Parse(costStr) > 5_000_000)
        {
            saga.RejectionReasons.Add("Cost exceeds allowed limit");
        }

        saga.Approved = !saga.RejectionReasons.Any();
    }


}

