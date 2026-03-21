using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteReceiptRequest>
{
    public async Task<IResult> HandleAsync(DeleteReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = m_Context.GetEntities<Receipt>()
            .SingleOrDefault(i => i.ReceiptID == request.ReceiptID);

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        if (_Receipt.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Receipt with Allocations.");

        var _RecieptItems = m_Context.GetEntities<ReceiptItem>().Where(i => i.ReceiptID == request.ReceiptID).ToList();

        m_Context.RemoveRange(_RecieptItems);
        m_Context.Remove(_Receipt);

        await m_Context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
