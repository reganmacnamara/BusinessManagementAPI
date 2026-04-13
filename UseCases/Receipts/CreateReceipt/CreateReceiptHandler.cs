using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;

public class CreateReceiptHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateReceiptRequest>
{
    public async Task<IResult> HandleAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Client = await context.GetEntities<Client>()
            .SingleAsync(c => c.ClientID == request.ClientID, cancellationToken);

        var _Receipt = mapper.Map<Receipt>(request);

        _Receipt.Client = _Client;
        _Receipt.Outstanding = true;

        context.Receipts.Add(_Receipt);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = mapper.Map<CreateReceiptResponse>(_Receipt);

        return Results.Created(string.Empty, _Response);
    }
}
