namespace Billing.Service.Application.Billing.CreateInvoice;

public record CreateInvoiceCommand(Guid OrderId, decimal Amount);
