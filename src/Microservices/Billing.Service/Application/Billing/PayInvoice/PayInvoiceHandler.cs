using Billing.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Billing.Service.Application.Billing.PayInvoice;

public class PayInvoiceHandler
{
    private readonly BillingDbContext _db;

    public PayInvoiceHandler(BillingDbContext db)
    {
        _db = db;
    }

    public async Task Handle(PayInvoiceCommand command)
    {
        var invoice = await _db.Invoices
            .Include(x => x.Payments)
            .FirstAsync(x => x.Id == command.InvoiceId);

        invoice.RegisterPayment(command.Amount);

        await _db.SaveChangesAsync();
    }
}
