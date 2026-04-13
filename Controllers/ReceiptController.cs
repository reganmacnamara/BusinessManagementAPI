using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;
using MacsBusinessManagementAPI.UseCases.Receipts.GetClientReceipts;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;
using MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
[EnableRateLimiting("Authenticated")]
public class ReceiptController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateReceipt([FromBody] CreateReceiptRequest request,
        [FromServices] PipelineMediator<CreateReceiptRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpDelete("{receiptID}")]
    public async Task<IResult> DeleteReceipt([FromRoute] long receiptID,
        [FromServices] PipelineMediator<DeleteReceiptRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ReceiptID = receiptID }, cancellationToken);

    [HttpDelete("Item/{receiptItemID}")]
    public async Task<IResult> DeleteReceiptItem([FromRoute] long receiptItemID,
        [FromServices] PipelineMediator<DeleteReceiptItemRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ReceiptItemID = receiptItemID }, cancellationToken);

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientReceipts([FromRoute] long clientID,
        [FromServices] PipelineMediator<GetClientReceiptsRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ClientID = clientID }, cancellationToken);

    [HttpGet("{receiptID}")]
    public async Task<IResult> GetReceipt([FromRoute] long receiptID,
        [FromServices] PipelineMediator<GetReceiptRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ReceiptID = receiptID }, cancellationToken);

    [HttpGet]
    public async Task<IResult> GetReceipts(
        [FromServices] PipelineMediator<GetReceiptsRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

    [HttpPatch]
    public async Task<IResult> UpdateReceipt([FromBody] UpdateReceiptRequest request,
        [FromServices] PipelineMediator<UpdateReceiptRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);

    [HttpGet("{receiptID}/pdf")]
    public async Task<IResult> GetReceiptPdf([FromRoute] long receiptID,
        [FromServices] PipelineMediator<GetReceiptPdfRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(new() { ReceiptID = receiptID }, cancellationToken);

    [HttpPut("Item")]
    public async Task<IResult> UpsertReceiptItem([FromBody] UpsertReceiptItemRequest request,
        [FromServices] PipelineMediator<UpsertReceiptItemRequest> mediator,
        CancellationToken cancellationToken)
        => await mediator.InvokeUseCaseAsync(request, cancellationToken);
}
