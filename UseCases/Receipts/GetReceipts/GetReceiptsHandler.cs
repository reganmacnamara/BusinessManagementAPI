using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(SQLContext context) : IUseCaseHandler<GetReceiptsRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptsRequest request, CancellationToken cancellationToken)
    {
        var _Response = new GetReceiptsResponse()
        {
            Receipts = [.. context.GetEntities<Receipt>().AsNoTracking().Include(r => r.Client)]
        };

        return Results.Ok(_Response);
    }
}
