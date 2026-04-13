using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptHandler(SQLContext context) : IUseCaseHandler<DeleteReceiptRequest>
{
    public async Task<IResult> HandleAsync(DeleteReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = await context.GetEntities<Receipt>()
            .SingleAsync(i => i.ReceiptID == request.ReceiptID, cancellationToken);

        if (_Receipt.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Receipt with Allocations.");

        var _RecieptItems = context.GetEntities<ReceiptItem>()
            .Where(i => i.ReceiptID == request.ReceiptID)
            .ToListAsync(cancellationToken);

        context.RemoveRange(_RecieptItems);
        context.Remove(_Receipt);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
