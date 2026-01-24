using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAutomationAPI.UseCases.TransactionItems.GetTransactionItems;

public class GetTransactionItemsHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task<GetTransactionItemsResponse> GetTransactionItems()
    {
        var _TransactionItems = await m_Context.TransactionItems.ToListAsync() ?? [];
        return m_Mapper.Map<GetTransactionItemsResponse>(_TransactionItems);
    }
}
