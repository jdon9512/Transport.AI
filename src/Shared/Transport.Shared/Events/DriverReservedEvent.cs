namespace Transport.Shared.Events;

public record DriverReservedEvent(Guid OrderId, Guid DriverId);
