using MassTransit;
using Transport.Shared.Events;
using Transport.AI.Agents;
using Transport.AI.Agents.Saga;
using Transport.AI.Infrastructure.Messaging;

namespace Transport.AI.Orchestrator.Orchestration;

public class OrchestratorCoordinator
{
    private readonly ISagaRepository _repo;
    private readonly LogisticsAgent _logistics;
    private readonly OperationsAgent _operations;
    private readonly FinanceAgent _finance;
    private readonly IEventBus _bus;

    public OrchestratorCoordinator(
        ISagaRepository repo,
        LogisticsAgent logistics,
        OperationsAgent operations,
        FinanceAgent finance,
        IEventBus bus)
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

        await _repo.SaveAsync(saga);

        if (!saga.Approved)
        {
            await _bus.Publish(new RejectOrderCommand
            {
                OrderId = saga.OrderId,
                Reasons = saga.RejectionReasons
            });

            return;
        }

        Guid.TryParse(saga.Operations?.Data["truckId"] ?? Guid.NewGuid().ToString(), out Guid truckId);
        Guid.TryParse(saga.Operations?.Data["driverId"] ?? Guid.NewGuid().ToString(), out Guid driverId);
        decimal.TryParse(saga.Finance?.Data["price"] ?? "200", out decimal price);

        await _bus.Publish(new ConfirmOrderCommand(saga.OrderId,truckId,driverId,price));
    }

    private void Validate(OrderSagaState saga)
    {
        //if (saga.Logistics == null || !saga.Logistics.Success)
        //    saga.RejectionReasons.Add("Invalid logistics decision");

        //if (saga.Operations == null || !saga.Operations.Success)
        //    saga.RejectionReasons.Add("No vehicle available");

        //if (saga.Finance == null || !saga.Finance.Success)
        //    saga.RejectionReasons.Add("Cost calculation failed");

        //// Ejemplo de regla cruzada
        //if (saga.Finance?.Data.TryGetValue("cost", out var costStr) == true
        //    && decimal.Parse(costStr) > 5_000_000)
        //{
        //    saga.RejectionReasons.Add("Cost exceeds allowed limit");
        //}

        saga.Approved = !saga.RejectionReasons.Any();
    }


}

