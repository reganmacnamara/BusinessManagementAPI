using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceHandler(SQLContext context) : IUseCaseHandler<GetInvoiceRequest>
{
    public async Task<IResult> HandleAsync(GetInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = await context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleAsync(cancellationToken);

        var _InvoiceItems = await context.GetEntities<InvoiceItem>()
            .AsNoTracking()
            .Include(ii => ii.Product)
            .Include(ii => ii.Service)
            .Where(ii => ii.InvoiceID == request.InvoiceID)
            .ToListAsync(cancellationToken);

        var _Response = new GetInvoiceResponse()
        {
            Invoice = _Invoice,
            InvoiceItems = _InvoiceItems
        };

        return Results.Ok(_Response);
    }
}
