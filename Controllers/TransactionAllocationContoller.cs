using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.TransactionAllocations.DeleteTransactionAllocation;
using BusinessManagementAPI.UseCases.TransactionAllocations.GetAllocationsByTransaction;
using BusinessManagementAPI.UseCases.TransactionAllocations.UpsertTransactionAllocation;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("TransactionAllocation")]
public class TransactionAllocationContoller(IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost("Delete")]
    public async Task<IResult> DeleteTransactionAllocation([FromBody] DeleteTransactionAllocationRequest request)
    {
        var handler = new DeleteTransactionAllocationHandler(mapper, context);

        var _Response = await handler.DeleteTransactionAllocation(request);

        return _Response;
    }

    [HttpGet("GetAllocationsByTransaction")]
    public async Task<IResult> GetAllocationsByTransaction([FromBody] GetAllocationsByTransactionRequest request)
    {
        var handler = new GetAllocationsByTransactionHandler(mapper, context);

        var _Response = await handler.GetAllocationsByTransaction(request);

        return _Response;
    }

    [HttpPost("Upsert")]
    public async Task<IResult> UpsertTransactionAllocation([FromBody] UpsertTransactionAllocationRequest request)
    {
        var handler = new UpsertTransactionAllocationHandler(mapper, context);

        var _Response = request.TransactionAllocationID != 0
            ? await handler.UpdateTransactionAllocation(request)
            : await handler.CreateTransactionAllocation(request);

        return _Response;
    }
}
