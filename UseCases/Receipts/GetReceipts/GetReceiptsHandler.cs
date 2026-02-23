using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetReceipts()
    {
        var _Response = new GetReceiptsResponse()
        {
            Receipts = [.. m_Context.Receipts.Include(r => r.Client)]
        };

        return Results.Ok(_Response);
    }
}
