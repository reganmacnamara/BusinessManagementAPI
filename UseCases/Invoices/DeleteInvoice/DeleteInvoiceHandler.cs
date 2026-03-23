using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceHandler(SQLContext context) : IUseCaseHandler<DeleteInvoiceRequest>
{
    public async Task<IResult> HandleAsync(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = await context.GetEntities<Invoice>()
            .SingleOrDefaultAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        if (_Invoice.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Invoice with Allocations.");

        var _InvoiceItems = await context.InvoiceItems
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToListAsync(cancellationToken);

        foreach (var item in _InvoiceItems)
            item.Product.QuantityOnHand += (long)item.Quantity;

        context.RemoveRange(_InvoiceItems);
        context.Remove(_Invoice);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
