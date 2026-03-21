using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoices;

public class GetInvoicesHandler(SQLContext context) : IUseCaseHandler<GetInvoicesRequest>
{
    public async Task<IResult> HandleAsync(GetInvoicesRequest request, CancellationToken cancellationToken)
    {
        var _Response = new GetInvoicesResponse()
        {
            Invoices = [.. context.GetEntities<Invoice>().AsNoTracking().Include(i => i.Client)]
        };

        return Results.Ok(_Response);
    }
}
