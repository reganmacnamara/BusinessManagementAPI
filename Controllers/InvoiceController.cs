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
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableRateLimiting("Authenticated")]
public class InvoiceController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateInvoice([FromBody] CreateInvoiceRequest request,
        [FromServices] PipelineMediator<CreateInvoiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpDelete("{invoiceID}")]
    public async Task<IResult> DeleteInvoice([FromRoute] long invoiceID,
        [FromServices] PipelineMediator<DeleteInvoiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { InvoiceID = invoiceID }, cancellationToken);

    [HttpDelete("Item/{invoiceItemID}")]
    public async Task<IResult> DeleteInvoiceItem([FromRoute] long invoiceItemID,
        [FromServices] PipelineMediator<DeleteInvoiceItemRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { InvoiceItemID = invoiceItemID }, cancellationToken);

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientInvoices([FromRoute] long clientID,
        [FromServices] PipelineMediator<GetClientInvoicesRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ClientID = clientID }, cancellationToken);

    [HttpGet("{invoiceID}")]
    public async Task<IResult> GetInvoice([FromRoute] long invoiceID,
        [FromServices] PipelineMediator<GetInvoiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { InvoiceID = invoiceID }, cancellationToken);

    [HttpGet]
    public async Task<IResult> GetInvoices(
        [FromServices] PipelineMediator<GetInvoicesRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

    [HttpPatch]
    public async Task<IResult> UpdateInvoice([FromBody] UpdateInvoiceRequest request,
        [FromServices] PipelineMediator<UpdateInvoiceRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpGet("{invoiceID}/pdf")]
    public async Task<IResult> GetInvoicePdf([FromRoute] long invoiceID,
        [FromServices] PipelineMediator<GetInvoicePdfRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { InvoiceID = invoiceID }, cancellationToken);

    [HttpPut("Item")]
    public async Task<IResult> UpsertInvoiceItem([FromBody] UpsertInvoiceItemRequest request,
        [FromServices] PipelineMediator<UpsertInvoiceItemRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);
}
