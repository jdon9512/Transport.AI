namespace Transport.Shared.Events;

public record TruckReservedEvent(Guid OrderId, Guid TruckId, Guid DriverId);
