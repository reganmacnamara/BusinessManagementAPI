using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Client = await context.GetEntities<Client>()
            .SingleOrDefaultAsync(c => c.ClientID == request.ClientID, cancellationToken);

        if (_Client is null)
            return Results.NotFound("Client not found.");

        var _Invoice = mapper.Map<Invoice>(request);

        _Invoice.Client = _Client;
        _Invoice.Outstanding = true;

        context.Invoices.Add(_Invoice);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = mapper.Map<CreateInvoiceResponse>(_Invoice);

        return Results.Created(string.Empty, _Response);
    }
}
