using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetTransactionsResponse> GetTransactions()
        {
            var _Transactions = m_Context.Transactions.ToList();

            var _Response = _Transactions.Count != 0
                ? m_Mapper.Map<GetTransactionsResponse>(_Transactions)
                : new GetTransactionsResponse();

            return _Response;
        }
    }

}
