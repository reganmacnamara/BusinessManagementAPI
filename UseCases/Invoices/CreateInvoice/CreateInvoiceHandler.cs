using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.DueDate;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateInvoiceRequest>
{
    public async Task<IResult> HandleAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var _Client = await context.GetEntities<Client>()
            .Include(c => c.PaymentTerm)
            .SingleOrDefaultAsync(c => c.ClientID == request.ClientID, cancellationToken);

        if (_Client is null)
            return Results.NotFound("Client not found.");

        var _Invoice = mapper.Map<Invoice>(request);

        _Invoice.Client = _Client;
        _Invoice.PaymentTerm = _Client.PaymentTerm;
        _Invoice.Outstanding = true;

        if (request.DueDate.HasValue)
            _Invoice.DueDate = request.DueDate.Value;
        else if (_Client.PaymentTerm is not null)
            _Invoice.DueDate = DueDateCalculator.CalculateDueDate(_Invoice.InvoiceDate, _Client.PaymentTerm);

        context.Invoices.Add(_Invoice);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = mapper.Map<CreateInvoiceResponse>(_Invoice);

        return Results.Created(string.Empty, _Response);
    }
}
