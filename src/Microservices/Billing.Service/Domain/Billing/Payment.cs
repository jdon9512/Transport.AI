namespace Billing.Service.Domain.Billing;

public class Payment
{
    public Guid Id { get; private set; }
    public Guid InvoiceId { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaidAt { get; private set; }

    private Payment() { }

    public static Payment Create(Guid invoiceId, decimal amount)
    {
        return new Payment
        {
            InvoiceId = invoiceId,
            Amount = amount,
            PaidAt = DateTime.UtcNow
        };
    }
}
