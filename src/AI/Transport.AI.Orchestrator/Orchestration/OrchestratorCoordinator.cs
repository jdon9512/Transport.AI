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

    public async Task Handle(OrderCreatedEvent order)
    {
        var saga = await _repo.Get(order.OrderId);

        var route = await _logistics.Calculate(order);
        var truck = await _operations.Assign(order);
        var cost = await _finance.Calculate(order, route.DistanceKm);

        saga.RouteReady = true;
        saga.TruckReady = true;
        saga.CostReady = true;

        saga.DistanceKm = route.DistanceKm;
        saga.TruckId = truck.TruckId;
        saga.DriverId = truck.DriverId;
        saga.Price = cost.Price;

        await _repo.Save(saga);

        await _bus.Publish(new ConfirmOrderCommand(
            saga.OrderId,
            saga.TruckId,
            saga.DriverId,
            saga.Price));
    }
}

