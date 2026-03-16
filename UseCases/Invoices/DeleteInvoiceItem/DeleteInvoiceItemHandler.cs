using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteInvoiceItem(DeleteInvoiceItemRequest request)
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
