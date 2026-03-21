using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<DeleteInvoiceRequest>
{
    public async Task<IResult> HandleAsync(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .SingleOrDefault(i => i.InvoiceID == request.InvoiceID);

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        if (_Invoice.OffsetValue != 0)
            return Results.Conflict("Cannot delete an Invoice with Allocations.");

        var _InvoiceItems = m_Context.InvoiceItems
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToList();

        foreach (var item in _InvoiceItems)
            item.Product.QuantityOnHand += (long)item.Quantity;

        m_Context.RemoveRange(_InvoiceItems);
        m_Context.Remove(_Invoice);

        await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
