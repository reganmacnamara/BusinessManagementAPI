using AutoMapper;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetTransactionsResponse> GetTransactions()
        {
            var _Transactions = await m_Context.Transactions.Include(t => t.Client).ToListAsync();

            var _Response = _Transactions.Count != 0
                ? m_Mapper.Map<GetTransactionsResponse>(_Transactions)
                : new GetTransactionsResponse();

            return _Response;
        }
    }

}
