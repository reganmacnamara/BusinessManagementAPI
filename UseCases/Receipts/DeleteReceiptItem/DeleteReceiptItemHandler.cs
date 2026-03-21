using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.Services.Allocations;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemHandler(IAllocationService allocationService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteReceiptItemRequest>
{
    public async Task<IResult> HandleAsync(DeleteReceiptItemRequest request, CancellationToken cancellationToken)
    {
        var _ReceiptItem = m_Context.GetEntities<ReceiptItem>()
            .Include(ri => ri.Invoice)
            .SingleOrDefault(ri => ri.ReceiptItemID == request.ReceiptItemID);

        if (_ReceiptItem is null)
            return Results.NotFound("Receipt Item not found.");

        await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

        m_Context.ReceiptItems.Remove(_ReceiptItem);

        _ = await m_Context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
