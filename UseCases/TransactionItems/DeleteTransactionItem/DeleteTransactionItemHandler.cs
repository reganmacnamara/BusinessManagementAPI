using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.TransactionItems.DeleteTransactionItem;

public class DeleteTransactionItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteTransactionItem(DeleteTransactionItemRequest request)
    {
        var _TransactionItem = m_Context.TransactionItems.FirstOrDefault(product => product.TransactionItemID == request.TransactionItemID);

        if (_TransactionItem is not null)
        {
            m_Context.TransactionItems.Remove(_TransactionItem);
            await m_Context.SaveChangesAsync();
            return Results.NoContent();
        }
        else
            return Results.NotFound("Transactin Item not found.");
    }
}
