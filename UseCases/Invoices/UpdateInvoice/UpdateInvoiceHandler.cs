using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<UpdateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .SingleOrDefault(i => i.InvoiceID == request.InvoiceID);

        if (_Invoice == null || request.InvoiceID == 0)
            return Results.NotFound("Invoice could not be found.");

        if (request.ClientID != _Invoice.ClientID)
        {
            var _Client = m_Context.GetEntities<Client>()
                .SingleOrDefault(c => c.ClientID == request.ClientID);

            if (_Client == null || request.ClientID == 0)
                return Results.NotFound("Client could not be found.");

            _Invoice.Client = _Client;
        }

        _Invoice = UpdateEntityFromRequest(_Invoice, request, ["InvoiceID"]);

        await m_Context.SaveChangesAsync();

        var _Response = new UpdateInvoiceResponse()
        {
            InvoiceID = _Invoice.InvoiceID
        };

        return Results.Ok(_Response);
    }
}
