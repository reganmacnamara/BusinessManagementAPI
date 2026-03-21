using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;
using MacsBusinessManagementAPI.UseCases.Receipts.GetClientReceipts;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;
using MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;
using MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;
using Microsoft.AspNetCore.Mvc;

namespace MacsBusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateReceipt([FromBody] CreateReceiptRequest request,
        [FromServices] IUseCaseHandler<CreateReceiptRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }

    [HttpDelete("{receiptID}")]
    public async Task<IResult> DeleteReceipt([FromRoute] long receiptID,
        [FromServices] IUseCaseHandler<DeleteReceiptRequest> handler,
        CancellationToken cancellationToken)
    {

        var _Response = await handler.HandleAsync(new() { ReceiptID = receiptID }, cancellationToken);

        return _Response;
    }

    [HttpDelete("Item/{receiptItemID}")]
    public async Task<IResult> DeleteReceiptItem([FromRoute] long receiptItemID,
        [FromServices] IUseCaseHandler<DeleteReceiptItemRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ReceiptItemID = receiptItemID }, cancellationToken);

        return _Response;
    }

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientReceipts([FromRoute] long clientID,
        [FromServices] IUseCaseHandler<GetClientReceiptsRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ClientID = clientID }, cancellationToken);

        return _Response;
    }

    [HttpGet("{receiptID}")]
    public async Task<IResult> GetReceipt([FromRoute] long receiptID,
        [FromServices] IUseCaseHandler<GetReceiptRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ReceiptID = receiptID }, cancellationToken);

        return _Response;
    }

    [HttpGet]
    public async Task<IResult> GetReceipts([FromServices] IUseCaseHandler<GetReceiptsRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new(), cancellationToken);

        return _Response;
    }

    [HttpPatch]
    public async Task<IResult> UpdateReceipt([FromBody] UpdateReceiptRequest request,
        [FromServices] IUseCaseHandler<UpdateReceiptRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }

    [HttpGet("{receiptID}/pdf")]
    public async Task<IResult> GetReceiptPdf([FromRoute] long receiptID,
        [FromServices] IUseCaseHandler<GetReceiptPdfRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(new() { ReceiptID = receiptID }, cancellationToken);

        return _Response;
    }

    [HttpPut("Item")]
    public async Task<IResult> UpsertReceiptItem([FromBody] UpsertReceiptItemRequest request,
        [FromServices] IUseCaseHandler<UpsertReceiptItemRequest> handler,
        CancellationToken cancellationToken)
    {
        var _Response = await handler.HandleAsync(request, cancellationToken);

        return _Response;
    }
}
