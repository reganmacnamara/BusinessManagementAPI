using AutoMapper;
using InvoiceAutomationAPI.UseCases.Base;

namespace InvoiceAutomationAPI.UseCases.TransactionItems.DeleteTransactionItem;

public class DeleteTransactionItemHandler(IMapper mapper) : BaseHandler(mapper)
{
    public async Task DeleteTransactionItem(DeleteTransactionItemRequest request)
    {
        var _TransactionItem = m_Context.TransactionItems.FirstOrDefault(product => product.TransactionItemID == request.TransactionItemID);

        if (_TransactionItem is not null)
        {
            m_Context.TransactionItems.Remove(_TransactionItem);
            await m_Context.SaveChangesAsync();
        }
        else
            throw new Exception("Transactin Item not found.");
    }
}
