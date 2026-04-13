using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceHandler(SQLContext context) : IUseCaseHandler<UpdateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Client = await context.GetEntities<Client>()
                .SingleAsync(c => c.ClientID == request.ClientID, cancellationToken);

        var _PaymentTerm = await context.GetEntities<PaymentTerm>()
           .SingleAsync(c => c.PaymentTermID == request.PaymentTermID, cancellationToken);

        var _Invoice = await context.GetEntities<Invoice>()
            .SingleAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

        _Invoice.Client = _Client;
        _Invoice.PaymentTerm = _PaymentTerm;

        _Invoice.UpdateFromEntity(request, [nameof(Invoice.InvoiceID)]);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateInvoiceResponse()
        {
            InvoiceID = _Invoice.InvoiceID
        };

        return Results.Ok(_Response);
    }
}
