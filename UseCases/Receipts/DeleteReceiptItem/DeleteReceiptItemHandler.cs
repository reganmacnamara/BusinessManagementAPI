using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteReceiptItem(DeleteReceiptItemRequest request)
    {
        var _ReceiptItem = await m_Context.ReceiptItems.FindAsync(request.ReceiptItemID);

        if (_ReceiptItem is null)
            return Results.NotFound("Receipt Item not found.");

        m_Context.ReceiptItems.Remove(_ReceiptItem);

        _ = await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
