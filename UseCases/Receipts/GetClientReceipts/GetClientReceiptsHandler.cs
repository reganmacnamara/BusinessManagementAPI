using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetClientReceipts;

public class GetClientReceiptsHandler(SQLContext context) : IUseCaseHandler<GetClientReceiptsRequest>
{
    public async Task<IResult> HandleAsync(GetClientReceiptsRequest request, CancellationToken cancellationToken)
    {
        var _Receipts = context.GetEntities<Receipt>()
            .AsNoTracking()
            .Include(r => r.Client)
            .Where(i => i.ClientID == request.ClientID)
            .ToList();

        var _Response = new GetClientReceiptsResponse()
        {
            Receipts = _Receipts
        };

        return Results.Ok(_Response);
    }
}
