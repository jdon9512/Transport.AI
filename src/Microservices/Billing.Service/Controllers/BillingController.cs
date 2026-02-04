using Billing.Service.Application.Billing.CreateInvoice;
using Billing.Service.Application.Billing.GetInvoice;
using Billing.Service.Application.Billing.PayInvoice;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Service.Controllers;

[ApiController]
[Route("api/billing")]
public class BillingController : ControllerBase
{
    private readonly CreateInvoiceHandler _create;
    private readonly PayInvoiceHandler _pay;
    private readonly GetInvoiceHandler _get;

    public BillingController(
        CreateInvoiceHandler create,
        PayInvoiceHandler pay,
        GetInvoiceHandler get)
    {
        _create = create;
        _pay = pay;
        _get = get;
    }

    [HttpPost("invoice")]
    public async Task<IActionResult> Create(CreateInvoiceCommand command)
    {
        var id = await _create.Handle(command);
        return Ok(id);
    }

    [HttpPost("pay")]
    public async Task<IActionResult> Pay(PayInvoiceCommand command)
    {
        await _pay.Handle(command);
        return Ok();
    }

    [HttpGet("invoice/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var invoice = await _get.Handle(id);
        return invoice == null ? NotFound() : Ok(invoice);
    }
}
