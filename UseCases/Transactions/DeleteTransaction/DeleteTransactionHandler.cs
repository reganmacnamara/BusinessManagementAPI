using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.DeleteTransaction
{

    public class DeleteTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> DeleteTransaction(DeleteTransactionRequest request)
        {
            var _Transaction = m_Context.Transactions.Find(request.TransactionID);

            if (_Transaction is not null)
            {
                var _TransactionAllocations = m_Context.TransactionAllocations.Where(ta => ta.AllocatingID == _Transaction.TransactionID
                    || ta.RecievingID == _Transaction.TransactionID);

                if (_TransactionAllocations.Any())
                    return Results.BadRequest("Cannot delete a Transaction that has allocations against it.");

                var _TransactionItems = m_Context.TransactionItems.Where(t => t.TransactionID == _Transaction.TransactionID);
                foreach (var _Item in _TransactionItems)
                    m_Context.TransactionItems.Remove(_Item);

                m_Context.Transactions.Remove(_Transaction);
                await m_Context.SaveChangesAsync();
                return Results.NoContent();
            }
            else
                return Results.NotFound("Transaction not found.");
        }
    }

}
