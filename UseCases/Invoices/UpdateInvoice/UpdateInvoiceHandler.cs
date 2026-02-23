using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> UpdateInvoice(UpdateInvoiceRequest request)
    {
        var _Invoice = m_Context.Invoices.Find(request.InvoiceID);

        if (_Invoice == null || request.InvoiceID == 0)
            return Results.NotFound("Invoice could not be found.");

        if (request.ClientID != _Invoice.ClientID)
        {
            var _Client = m_Context.Clients.Find(request.ClientID);

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
