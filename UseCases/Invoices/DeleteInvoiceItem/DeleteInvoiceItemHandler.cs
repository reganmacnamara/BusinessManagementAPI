using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemHandler(SQLContext context) : IUseCaseHandler<DeleteInvoiceItemRequest>
{
    public async Task<IResult> HandleAsync(DeleteInvoiceItemRequest request, CancellationToken cancellationToken)
    {
        var _InvoiceItem = await context.GetEntities<InvoiceItem>()
            .Include(ii => ii.Product)
            .SingleAsync(ii => ii.InvoiceItemID == request.InvoiceItemID, cancellationToken);

        _InvoiceItem.Product.QuantityOnHand += (long)_InvoiceItem.Quantity;

        context.InvoiceItems.Remove(_InvoiceItem);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
