namespace Transport.Shared.Events;

public record CostCalculatedEvent(Guid OrderId, decimal Price);
