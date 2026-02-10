using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionAllocations.DeleteTransactionAllocation;

public class DeleteTransactionAllocationHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteTransactionAllocation(DeleteTransactionAllocationRequest request)
    {
        var _TransactionAllocation = m_Context.TransactionAllocations.SingleOrDefault(t => t.TransactionAllocationID == request.TransactionAllocationID);

        if (_TransactionAllocation is null)
            return Results.NotFound("Allocation could not be found.");

        var _AllocatingTransaction = m_Context.Transactions.SingleOrDefault(t => t.TransactionID == _TransactionAllocation.AllocatingID);
        var _RecievingTransaction = m_Context.Transactions.SingleOrDefault(t => t.TransactionID == _TransactionAllocation.RecievingID);

        if (_AllocatingTransaction is null)
            return Results.NotFound("Allocating Transaction not found.");

        if (_RecievingTransaction is null)
            return Results.NotFound("Recieving Transaction not found.");

        _AllocatingTransaction.OffsetingValue -= _AllocatingTransaction.OffsetValue;

        if (_AllocatingTransaction.OffsetingValue == _AllocatingTransaction.NetValue)
            _AllocatingTransaction.Outstanding = false;
        else
            _AllocatingTransaction.Outstanding = true;

        _RecievingTransaction.OffsetValue += _AllocatingTransaction.OffsetValue;

        if (_RecievingTransaction.OffsetValue == _RecievingTransaction.NetValue)
            _RecievingTransaction.Outstanding = false;
        else
            _RecievingTransaction.Outstanding = true;

        m_Context.TransactionAllocations.Remove(_TransactionAllocation);

        await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
