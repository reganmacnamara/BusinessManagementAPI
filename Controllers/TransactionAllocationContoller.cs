using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.TransactionAllocations.CreateTransactionAllocation;
using BusinessManagementAPI.UseCases.TransactionAllocations.DeleteTransactionAllocation;
using BusinessManagementAPI.UseCases.TransactionAllocations.GetAllocationsByTransaction;
using BusinessManagementAPI.UseCases.TransactionAllocations.GetTransactionAllocationsByTransaction;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionAllocationContoller(IMapper mapper, SQLContext context) : ControllerBase
{
    [HttpPost("Create")]
    public async Task<IResult> CreateTransactionAllocation([FromBody] CreateTransactionAllocationRequest request)
    {
        var handler = new CreateTransactionAllocationHandler(mapper, context);

        var _Response = await handler.CreateTransactionAllocation(request);

        return _Response;
    }

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
}
