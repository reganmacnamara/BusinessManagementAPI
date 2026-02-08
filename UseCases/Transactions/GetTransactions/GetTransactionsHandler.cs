using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> GetTransactions()
        {
            var _Transactions = m_Context.Transactions.Include(t => t.Client).ToList();

            var _Response = _Transactions.Count != 0
                ? m_Mapper.Map<GetTransactionsResponse>(_Transactions)
                : new GetTransactionsResponse();

            return Results.Ok(_Response);
        }
    }

}
