using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteInvoiceItem(DeleteInvoiceItemRequest request)
    {
        var _InvoiceItem = await m_Context.InvoiceItems.FindAsync(request.InvoiceItemID);

        if (_InvoiceItem is null)
            return Results.NotFound("Invoice Item not found.");

        m_Context.InvoiceItems.Remove(_InvoiceItem);

        _ = await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
