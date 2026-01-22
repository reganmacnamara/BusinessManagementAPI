using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetTransaction
{

    public class GetTransactionHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetTransactionResponse> GetTransaction(GetTransactionRequest request)
        {
            var _Transaction = await m_Context.Transactions
                .Include(transaction => transaction.Client)
                .Where(transaction => transaction.TransactionID == request.TransactionID)
                .SingleAsync();

            var _Response = m_Mapper.Map<GetTransactionResponse>(_Transaction);

            return _Response;
        }
    }

}
