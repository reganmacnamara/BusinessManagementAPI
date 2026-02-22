using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> CreateInvoice(CreateInvoiceRequest request)
    {
        var _Client = m_Context.GetEntities<Client>()
            .Where(c => c.ClientID == request.ClientID)
            .Include(c => c.Invoices)
            .SingleOrDefault();

        if (_Client is null)
            return Results.NotFound("Client not found.");

        var _Invoice = m_Mapper.Map<Invoice>(request);

        _Invoice.Client = _Client;
        _Invoice.Outstanding = true;

        m_Context.Invoices.Add(_Invoice);
        _Client.Invoices.Add(_Invoice);

        _ = await m_Context.SaveChangesAsync();

        var _Response = m_Mapper.Map<CreateInvoiceResponse>(_Invoice);

        return Results.Created(string.Empty, _Response);
    }
}
