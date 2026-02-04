namespace Transport.Shared.Events;

public record ConfirmOrderCommand(Guid OrderId, Guid TruckId, Guid DriverId, decimal Price);