using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Receipts.CreateReceipt;
using BusinessManagementAPI.UseCases.Receipts.DeleteReceipt;
using BusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;
using BusinessManagementAPI.UseCases.Receipts.GetClientReceipts;
using BusinessManagementAPI.UseCases.Receipts.GetReceipt;
using BusinessManagementAPI.UseCases.Receipts.GetReceipts;
using BusinessManagementAPI.UseCases.Receipts.UpdateReceipt;
using BusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReceiptController(IAllocationService allocationService, IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost]
    public async Task<IResult> CreateReceipt([FromBody] CreateReceiptRequest request)
    {
        var handler = new CreateReceiptHandler(mapper, context);

        var _Response = await handler.CreateReceipt(request);

        return _Response;
    }

    [HttpDelete("{receiptID}")]
    public async Task<IResult> DeleteReceipt([FromRoute] long receiptID)
    {
        var _Request = new DeleteReceiptRequest()
        {
            ReceiptID = receiptID
        };

        var handler = new DeleteReceiptHandler(mapper, context);

        var _Response = await handler.DeleteReceipt(_Request);

        return _Response;
    }

    [HttpDelete("Item/{receiptItemID}")]
    public async Task<IResult> DeleteReceiptItem([FromRoute] long receiptItemID)
    {
        var _Request = new DeleteReceiptItemRequest()
        {
            ReceiptItemID = receiptItemID
        };

        var handler = new DeleteReceiptItemHandler(mapper, context);

        var _Response = await handler.DeleteReceiptItem(_Request);

        return _Response;
    }

    [HttpGet("Client/{clientID}")]
    public async Task<IResult> GetClientReceipts([FromRoute] long clientID)
    {
        var _Request = new GetClientReceiptsRequest()
        {
            ClientID = clientID
        };

        var handler = new GetClientReceiptsHandler(mapper, context);

        var _Response = await handler.GetClientReceipts(_Request);

        return _Response;
    }

    [HttpGet("{receiptID}")]
    public async Task<IResult> GetReceipt([FromRoute] long receiptID)
    {
        var _Request = new GetReceiptRequest()
        {
            ReceiptID = receiptID
        };

        var handler = new GetReceiptHandler(mapper, context);

        var _Response = await handler.GetReceipt(_Request);

        return _Response;
    }

    [HttpGet]
    public async Task<IResult> GetReceipts()
    {
        var handler = new GetReceiptsHandler(mapper, context);

        var _Response = await handler.GetReceipts();

        return _Response;
    }

    [HttpPatch]
    public async Task<IResult> UpdateReceipt([FromBody] UpdateReceiptRequest request)
    {
        var handler = new UpdateReceiptHandler(mapper, context);

        var _Response = await handler.UpdateReceipt(request);

        return _Response;
    }

    [HttpPut("Item")]
    public async Task<IResult> UpsertReceiptItem([FromBody] UpsertReceiptItemRequest request)
    {
        var handler = new UpsertReceiptItemHandler(allocationService, mapper, context);

        var _Response = await handler.UpdateReceiptItem(request);

        return _Response;
    }
}
