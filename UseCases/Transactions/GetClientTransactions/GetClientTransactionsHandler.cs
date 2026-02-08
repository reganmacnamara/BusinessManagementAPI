using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Transactions.GetClientTransactions
{

    public class GetClientTransactionsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> GetClientTransactions(GetClientTransactionsRequest request)
        {
            var _Transactions = m_Context.Transactions.Where(t => t.ClientID == request.ClientID).ToList();

            return Results.Ok(m_Mapper.Map<GetClientTransactionsResponse>(_Transactions));
        }
    }

}
