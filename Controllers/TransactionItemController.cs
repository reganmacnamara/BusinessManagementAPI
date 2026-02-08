using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.TransactionItems.DeleteTransactionItem;
using BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;
using BusinessManagementAPI.UseCases.TransactionItems.UpsertTransactionItem;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionItemController(IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost("Delete")]
    public async Task<IResult> DeleteTransactionItem([FromBody] DeleteTransactionItemRequest request)
    {
        var handler = new DeleteTransactionItemHandler(mapper, context);

        var _Response = await handler.DeleteTransactionItem(request);

        return _Response;
    }

    [HttpGet]
    public async Task<IResult> GetTransactions()
    {
        var handler = new GetTransactionItemsHandler(mapper, context);

        var _Response = await handler.GetTransactionItems();

        return _Response;
    }

    [HttpPost("Upsert")]
    public async Task<IResult> UpsertTransactionItem([FromBody] UpsertTransactionItemRequest request)
    {
        var handler = new UpsertTransactionItemHandler(mapper, context);

        var _Result = request.TransactionItemID != 0
            ? await handler.UpdateTransactionItem(request)
            : await handler.CreateTransactionItem(request);

        return _Result;
    }
}
