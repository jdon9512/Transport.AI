namespace Billing.Service.Domain.Billing;

public class Invoice
{
    public Guid Id { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsPaid { get; private set; }

    public List<Payment> Payments { get; private set; } = [];

    private Invoice() { }

    public static Invoice Create(Guid orderId, decimal amount)
    {
        return new Invoice
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Amount = amount,
            IsPaid = false
        };
    }

    public void RegisterPayment(decimal amount)
    {
        Payments.Add(Payment.Create(Id, amount));

        if (Payments.Sum(x => x.Amount) >= Amount)
            IsPaid = true;
    }
}
