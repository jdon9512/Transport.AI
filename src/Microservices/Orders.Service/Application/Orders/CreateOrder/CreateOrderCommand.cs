namespace Orders.Service.Application.Orders.CreateOrder
{
    public record CreateOrderCommand(
        string Origin,
        string Destination,
        string CargoDescription,
        decimal WeightKg);

}
