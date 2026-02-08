using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Transactions.GetTransaction
{

    public class GetTransactionHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> GetTransaction(GetTransactionRequest request)
        {
            var _Transaction = m_Context.Transactions
                .Include(transaction => transaction.Client)
                .Where(transaction => transaction.TransactionID == request.TransactionID)
                .SingleOrDefault();

            if (_Transaction == null)
                return Results.NotFound("Transaction not found.");

            var _TranactionItems = m_Context.TransactionItems
                .Include(item => item.Product)
                .Where(item => item.TransactionID == request.TransactionID).ToList();

            var _Response = m_Mapper.Map<GetTransactionResponse>(_Transaction);

            _Response.TransactionItems = _TranactionItems;

            return Results.Ok(_Response);
        }
    }

}
