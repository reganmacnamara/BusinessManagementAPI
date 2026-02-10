using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using BusinessManagementAPI.UseCases.TransactionAllocations.GetTransactionAllocationsByTransaction;

namespace BusinessManagementAPI.UseCases.TransactionAllocations.GetAllocationsByTransaction;

public class GetAllocationsByTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetAllocationsByTransaction(GetAllocationsByTransactionRequest request)
    {
        var _TransactionAllocations = (request.IsReciever
            ? m_Context.TransactionAllocations.Where(t => t.RecievingID == request.TransactionID)
            : m_Context.TransactionAllocations.Where(t => t.AllocatingID == request.TransactionID)).ToList();

        var _Response = new GetAllocationsByTransactionResponse()
        {
            TransactionAllocations = _TransactionAllocations
        };

        return Results.Ok(_Response);
    }
}
