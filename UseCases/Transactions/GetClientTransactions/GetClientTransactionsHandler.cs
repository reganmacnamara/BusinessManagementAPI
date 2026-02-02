using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.GetClientTransactions
{

    public class GetClientTransactionsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<GetClientTransactionsResponse> GetClientTransactions(GetClientTransactionsRequest request)
        {
            var _Transactions = m_Context.Transactions.Where(t => t.ClientID == request.ClientID).ToList();

            return m_Mapper.Map<GetClientTransactionsResponse>(_Transactions);
        }
    }

}
