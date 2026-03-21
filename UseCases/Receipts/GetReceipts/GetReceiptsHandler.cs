using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetReceiptsRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptsRequest request, CancellationToken cancellationToken)
    {
        var _Response = new GetReceiptsResponse()
        {
            Receipts = [.. m_Context.GetEntities<Receipt>().AsNoTracking().Include(r => r.Client)]
        };

        return Results.Ok(_Response);
    }
}
