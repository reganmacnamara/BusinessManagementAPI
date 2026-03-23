using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetClientInvoices;

public class GetClientInvoicesHandler(SQLContext context) : IUseCaseHandler<GetClientInvoicesRequest>
{
    public async Task<IResult> HandleAsync(GetClientInvoicesRequest request, CancellationToken cancellationToken)
    {
        var _Invoices = await context.GetEntities<Invoice>()
            .AsNoTracking()
            .Include(i => i.Client)
            .Where(i => i.ClientID == request.ClientID)
            .ToListAsync(cancellationToken);

        var _Response = new GetClientInvoicesResponse()
        {
            Invoices = _Invoices
        };

        return Results.Ok(_Response);
    }
}
