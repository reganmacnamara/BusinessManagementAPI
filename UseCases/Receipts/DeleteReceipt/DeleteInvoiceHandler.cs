using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptHandler(SQLContext context) : IUseCaseHandler<DeleteReceiptRequest>
{
    public async Task<IResult> HandleAsync(DeleteReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = context.GetEntities<Receipt>()
            .SingleOrDefault(i => i.ReceiptID == request.ReceiptID);

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        if (_Receipt.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Receipt with Allocations.");

        var _RecieptItems = context.GetEntities<ReceiptItem>().Where(i => i.ReceiptID == request.ReceiptID).ToList();

        context.RemoveRange(_RecieptItems);
        context.Remove(_Receipt);

        await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
