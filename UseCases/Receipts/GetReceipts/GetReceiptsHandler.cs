using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(SQLContext context) : IUseCaseHandler<GetReceiptsRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptsRequest request, CancellationToken cancellationToken)
    {
        var _Receipts = await context.GetEntities<Receipt>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var _Response = new GetReceiptsResponse()
        {
            Receipts = _Receipts
        };

        return Results.Ok(_Response);
    }
}
