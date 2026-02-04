namespace Transport.Shared.Events;

public record InvoiceCreatedEvent(Guid OrderId);
