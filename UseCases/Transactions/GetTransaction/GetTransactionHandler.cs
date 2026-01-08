using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetTransaction
{

    public class GetTransactionHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetTransactionResponse> GetTransaction(GetTransactionRequest request)
        {
            var _Transaction = m_Context.Transactions.Find(request.TransactionID);

            var _Response = m_Mapper.Map<GetTransactionResponse>(_Transaction);

            return _Response;
        }
    }

}
