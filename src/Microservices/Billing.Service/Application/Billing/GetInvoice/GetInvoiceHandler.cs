using Billing.Service.Domain.Billing;
using Billing.Service.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Billing.Service.Application.Billing.GetInvoice;

public class GetInvoiceHandler
{
    private readonly BillingDbContext _db;

    public GetInvoiceHandler(BillingDbContext db)
    {
        _db = db;
    }

    public async Task<Invoice?> Handle(Guid id)
    {
        return await _db.Invoices
            .Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
