using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;
using MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;
using MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;
using MacsBusinessManagementAPI.UseCases.Invoices.GetClientInvoices;
using MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;
using MacsBusinessManagementAPI.UseCases.Invoices.GetInvoicePdf;
using MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;
using MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;
using MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class InvoiceController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateInvoice([FromBody] CreateInvoiceRequest request,
        [FromServices] IUseCaseHandler<CreateInvoiceRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }

    [HttpDelete("{invoiceID}")]
    public async Task<IResult> DeleteInvoice([FromRoute] long invoiceID,
        [FromServices] IUseCaseHandler<DeleteInvoiceRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { InvoiceID = invoiceID }, cancellationToken);

        return _Response;
    }

    [HttpDelete("Item/{invoiceItemID}")]
    public async Task<IResult> DeleteInvoiceItem([FromRoute] long invoiceItemID,
        [FromServices] IUseCaseHandler<DeleteInvoiceItemRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { InvoiceItemID = invoiceItemID }, cancellationToken);

        return _Response;
    }

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientInvoices([FromRoute] long clientID,
        [FromServices] IUseCaseHandler<GetClientInvoicesRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ClientID = clientID }, cancellationToken);

        return _Response;
    }

    [HttpGet("{invoiceID}")]
    public async Task<IResult> GetInvoice([FromRoute] long invoiceID,
        [FromServices] IUseCaseHandler<GetInvoiceRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { InvoiceID = invoiceID }, cancellationToken);

        return _Response;
    }

    [HttpGet]
    public async Task<IResult> GetInvoices([FromServices] IUseCaseHandler<GetInvoicesRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new(), cancellationToken);

        return _Response;
    }

    [HttpPatch]
    public async Task<IResult> UpdateInvoice([FromBody] UpdateInvoiceRequest request,
        [FromServices] IUseCaseHandler<UpdateInvoiceRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }

    [HttpGet("{invoiceID}/pdf")]
    public async Task<IResult> GetInvoicePdf([FromRoute] long invoiceID,
        [FromServices] IUseCaseHandler<GetInvoicePdfRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { InvoiceID = invoiceID }, cancellationToken);

        return _Response;
    }

    [HttpPut("Item")]
    public async Task<IResult> UpsertInvoiceItem([FromBody] UpsertInvoiceItemRequest request,
        [FromServices] IUseCaseHandler<UpsertInvoiceItemRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }
}
