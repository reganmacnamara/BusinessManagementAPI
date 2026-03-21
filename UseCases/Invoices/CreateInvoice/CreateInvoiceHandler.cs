using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<CreateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Client = m_Context.GetEntities<Client>()
            .SingleOrDefault(c => c.ClientID == request.ClientID);

        if (_Client is null)
            return Results.NotFound("Client not found.");

        var _Invoice = m_Mapper.Map<Invoice>(request);

        _Invoice.Client = _Client;
        _Invoice.Outstanding = true;

        m_Context.Invoices.Add(_Invoice);

        _ = await m_Context.SaveChangesAsync(cancellationToken);

        var _Response = m_Mapper.Map<CreateInvoiceResponse>(_Invoice);

        return Results.Created(string.Empty, _Response);
    }
}
