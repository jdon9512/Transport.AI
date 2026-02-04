namespace Billing.Service.Application.Billing.PayInvoice;

public record PayInvoiceCommand(Guid InvoiceId, decimal Amount);
