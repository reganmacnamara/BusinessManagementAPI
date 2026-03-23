using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Services.Allocations;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemHandler(IAllocationService allocationService, SQLContext context) : IUseCaseHandler<DeleteReceiptItemRequest>
{
    public async Task<IResult> HandleAsync(DeleteReceiptItemRequest request, CancellationToken cancellationToken)
    {
        var _ReceiptItem = await context.GetEntities<ReceiptItem>()
            .Include(ri => ri.Invoice)
            .SingleOrDefaultAsync(ri => ri.ReceiptItemID == request.ReceiptItemID, cancellationToken);

        if (_ReceiptItem is null)
            return Results.NotFound("Receipt Item not found.");

        await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

        context.ReceiptItems.Remove(_ReceiptItem);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
