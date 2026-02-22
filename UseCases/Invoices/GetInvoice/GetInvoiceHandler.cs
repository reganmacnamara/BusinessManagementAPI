using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetInvoice(GetInvoiceRequest request)
    {
        var _Invoice = m_Context.GetEntities<Invoice>()
            .Include(i => i.InvoiceItems)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefault();

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        var _Response = new GetInvoiceResponse()
        {
            Invoice = _Invoice,
            InvoiceItems = _Invoice.InvoiceItems
        };

        return Results.Ok(_Response);
    }
}
