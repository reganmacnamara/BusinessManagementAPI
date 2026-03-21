using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetInvoiceRequest>
{
    public async Task<IResult> HandleAsync(GetInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefault();

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        var _InvoiceItems = m_Context.GetEntities<InvoiceItem>()
            .AsNoTracking()
            .Include(ii => ii.Product)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToList();

        var _Response = new GetInvoiceResponse()
        {
            Invoice = _Invoice,
            InvoiceItems = _InvoiceItems
        };

        return Results.Ok(_Response);
    }
}
