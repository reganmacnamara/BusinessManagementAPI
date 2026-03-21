using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteInvoiceItemRequest>
{
    public async Task<IResult> HandleAsync(DeleteInvoiceItemRequest request, CancellationToken cancellation)
    {
        var _InvoiceItem = m_Context.GetEntities<InvoiceItem>()
            .Include(ii => ii.Product)
            .SingleOrDefault(ii => ii.InvoiceItemID == request.InvoiceItemID);

        if (_InvoiceItem is null)
            return Results.NotFound("Invoice Item not found.");

        _InvoiceItem.Product.QuantityOnHand += (long)_InvoiceItem.Quantity;

        m_Context.InvoiceItems.Remove(_InvoiceItem);

        _ = await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
