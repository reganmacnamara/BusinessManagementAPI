using AutoMapper;
using InvoiceAutomationAPI.UseCases.TransactionItems.CreateTransactionItem;
using InvoiceAutomationAPI.UseCases.TransactionItems.DeleteTransactionItem;
using InvoiceAutomationAPI.UseCases.TransactionItems.GetTransactionItems;
using InvoiceAutomationAPI.UseCases.TransactionItems.UpdateTransactionItem;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceAutomationAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionItemController(IMapper mapper) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IResult> CreateTransactionItem([FromBody] CreateTransactionItemRequest request)
    {
        var handler = new CreateTransactionItemHandler(mapper);

        var _Response = await handler.CreateTransactionItem(request);

        return _Response is not null
            ? Results.Ok(_Response)
            : Results.BadRequest();
    }

    [HttpPost("Delete")]
    public async Task<IResult> DeleteTransactionItem([FromBody] DeleteTransactionItemRequest request)
    {
        var handler = new DeleteTransactionItemHandler(mapper);

        await handler.DeleteTransactionItem(request);

        return Results.NoContent();
    }

    [HttpGet]
    public async Task<IResult> GetTransactions()
    {
        var handler = new GetTransactionItemsHandler(mapper);

        var _Response = await handler.GetTransactionItems();

        return Results.Ok(_Response);
    }

    [HttpPost("Update")]
    public async Task<IResult> UpdateTransactionItem([FromBody] UpdateTransactionItemRequest request)
    {
        var handler = new UpdateTransactionItemHandler(mapper);

        var _Result = await handler.UpdateTransactionItem(request);

        return _Result.TransactionItemID != 0
            ? Results.Ok(_Result)
            : Results.NotFound();
    }
}
