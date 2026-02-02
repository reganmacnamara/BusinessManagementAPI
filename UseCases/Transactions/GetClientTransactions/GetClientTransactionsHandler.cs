using AutoMapper;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.GetClientTransactions
{

    public class GetClientTransactionsHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetClientTransactionsResponse> GetClientTransactions(GetClientTransactionsRequest request)
        {
            var _Transactions = m_Context.Transactions.Where(t => t.ClientID == request.ClientID).ToList();

            return m_Mapper.Map<GetClientTransactionsResponse>(_Transactions);
        }
    }

}
