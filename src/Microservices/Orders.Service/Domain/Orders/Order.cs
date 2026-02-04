namespace Orders.Service.Domain.Orders;

public class Order
{
    public Guid Id { get; private set; }
    public string Origin { get; private set; }
    public string Destination { get; private set; }
    public string CargoDescription { get; private set; }
    public decimal WeightKg { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Order() { }

    public static Order Create(
        string origin,
        string destination,
        string cargo,
        decimal weight)
    {
        return new Order
        {
            Id = Guid.NewGuid(),
            Origin = origin,
            Destination = destination,
            CargoDescription = cargo,
            WeightKg = weight,
            Status = OrderStatus.Created,
            CreatedAt = DateTime.UtcNow
        };
    }
    public void Confirm()//Guid truckId, Guid driverId, decimal price
    {
        Status = OrderStatus.Confirmed;
    }
}
