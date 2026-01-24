using AutoMapper;
using InvoiceAutomationAPI.UseCases.TransactionItems.CreateTransactionItem;
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
}
