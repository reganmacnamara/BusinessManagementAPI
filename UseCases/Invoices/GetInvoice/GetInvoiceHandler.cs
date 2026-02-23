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
            .Include(i => i.Client)
            .Where(i => i.InvoiceID == request.InvoiceID)
            .SingleOrDefault();

        if (_Invoice is null)
            return Results.NotFound($"Invoice {request.InvoiceID} could not be found.");

        var _InvoiceItems = m_Context.InvoiceItems
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
