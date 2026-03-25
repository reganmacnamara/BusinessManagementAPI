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
                .SingleOrDefaultAsync(c => c.ClientID == request.ClientID, cancellationToken);

        if (_Client == null || request.ClientID == 0)
            return Results.NotFound("Client could not be found.");

        var _PaymentTerm = await context.GetEntities<PaymentTerm>()
           .SingleOrDefaultAsync(c => c.PaymentTermID == request.PaymentTermID, cancellationToken);

        if (_PaymentTerm is null)
            return Results.NotFound("Payment Term not found.");

        var _Invoice = await context.GetEntities<Invoice>()
            .SingleOrDefaultAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

        if (_Invoice == null || request.InvoiceID == 0)
            return Results.NotFound("Invoice could not be found.");

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
