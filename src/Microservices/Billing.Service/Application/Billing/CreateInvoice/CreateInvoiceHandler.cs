using Billing.Service.Domain.Billing;
using Billing.Service.Infrastructure.Persistence;

namespace Billing.Service.Application.Billing.CreateInvoice;

public class CreateInvoiceHandler
{
    private readonly BillingDbContext _db;

    public CreateInvoiceHandler(BillingDbContext db)
    {
        _db = db;
    }

    public async Task<Guid> Handle(CreateInvoiceCommand command)
    {
        var invoice = Invoice.Create(command.OrderId, command.Amount);

        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync();

        return invoice.Id;
    }
}
