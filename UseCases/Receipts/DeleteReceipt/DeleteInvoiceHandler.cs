using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteReceipt(DeleteReceiptRequest request)
    {
        var _Receipt = m_Context.GetEntities<Receipt>()
            .Where(i => i.ReceiptID == request.ReceiptID)
            .SingleOrDefault();

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        if (_Receipt.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Receipt with Allocations.");

        var _RecieptItems = m_Context.ReceiptItems.Where(i => i.ReceiptID == request.ReceiptID).ToList();

        m_Context.RemoveRange(_RecieptItems);
        m_Context.Remove(_Receipt);

        await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
