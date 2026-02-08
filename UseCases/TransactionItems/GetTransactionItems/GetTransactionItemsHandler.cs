using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;

public class GetTransactionItemsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetTransactionItems()
    {
        var _TransactionItems = await m_Context.TransactionItems
            .Include(item => item.Transaction)
            .Include(item => item.Product)
            .ToListAsync() ?? [];

        return Results.Ok(m_Mapper.Map<GetTransactionItemsResponse>(_TransactionItems));
    }
}
