using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceHandler(SQLContext context) : IUseCaseHandler<UpdateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = context.GetEntities<Invoice>()
            .SingleOrDefault(i => i.InvoiceID == request.InvoiceID);

        if (_Invoice == null || request.InvoiceID == 0)
            return Results.NotFound("Invoice could not be found.");

        if (request.ClientID != _Invoice.ClientID)
        {
            var _Client = context.GetEntities<Client>()
                .SingleOrDefault(c => c.ClientID == request.ClientID);

            if (_Client == null || request.ClientID == 0)
                return Results.NotFound("Client could not be found.");

            _Invoice.Client = _Client;
        }

        _Invoice.UpdateFromEntity(request, ["InvoiceID"]);

        await context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateInvoiceResponse()
        {
            InvoiceID = _Invoice.InvoiceID
        };

        return Results.Ok(_Response);
    }
}
