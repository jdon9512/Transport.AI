namespace Transport.AI.Orchestrator.Saga;

public class OrderSagaState
{
    public Guid OrderId { get; set; }

    public bool RouteReady { get; set; }
    public bool TruckReady { get; set; }
    public bool CostReady { get; set; }

    public double DistanceKm { get; set; }
    public Guid TruckId { get; set; }
    public Guid DriverId { get; set; }
    public decimal Price { get; set; }
}
