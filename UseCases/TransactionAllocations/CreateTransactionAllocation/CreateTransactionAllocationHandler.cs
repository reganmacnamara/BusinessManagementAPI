using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionAllocations.CreateTransactionAllocation;

public class CreateTransactionAllocationHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateTransactionAllocation(CreateTransactionAllocationRequest request)
    {
        var _AllocatingTransaction = m_Context.Transactions.SingleOrDefault(t => t.TransactionID == request.AllocatingID);
        var _RecievingTransaction = m_Context.Transactions.SingleOrDefault(t => t.TransactionID == request.RecievingID);

        if (_AllocatingTransaction is null)
            return Results.NotFound("Allocating Transaction not found.");

        if (_RecievingTransaction is null)
            return Results.NotFound("Recieving Transaction not found.");

        var _TransactionAllocation = m_Mapper.Map<TransactionAllocation>(request);

        if (_TransactionAllocation is null)
            return Results.BadRequest("Transaction Allocation could not be created.");

        m_Context.TransactionAllocations.Add(_TransactionAllocation);

        _AllocatingTransaction.OffsetingValue += _AllocatingTransaction.OffsetValue;
        _RecievingTransaction.OffsetValue -= _AllocatingTransaction.OffsetValue;

        if (_AllocatingTransaction.OffsetingValue == _AllocatingTransaction.NetValue)
            _AllocatingTransaction.Outstanding = false;
        else
            _AllocatingTransaction.Outstanding = true;

        if (_RecievingTransaction.OffsetValue == _RecievingTransaction.NetValue)
            _RecievingTransaction.Outstanding = false;
        else
            _RecievingTransaction.Outstanding = true;

        await m_Context.SaveChangesAsync();

        var _Response = new CreateTransactionAllocationResponse()
        {
            TransactionAllocationID = _TransactionAllocation.TransactionAllocationID,
        };

        return Results.Ok(_Response);
    }
}
