using AutoMapper;
using MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetReceipts()
    {
        var _Response = new GetReceiptsResponse()
        {
            Receipts = [.. m_Context.GetEntities<Receipt>().AsNoTracking().Include(r => r.Client)]
        };

        return Results.Ok(_Response);
    }
}
