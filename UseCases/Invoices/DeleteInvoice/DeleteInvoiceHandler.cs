using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteInvoice(DeleteInvoiceRequest request)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .Include(i => i.InvoiceItems)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefault();

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        if (_Invoice.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Invoice with Allocations.");

        m_Context.RemoveRange(_Invoice.InvoiceItems);
        m_Context.Remove(_Invoice);

        await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
