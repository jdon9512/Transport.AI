namespace Transport.Shared.Events;

public record RouteCalculatedEvent(Guid OrderId, double DistanceKm);
