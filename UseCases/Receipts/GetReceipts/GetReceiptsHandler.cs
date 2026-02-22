using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetReceipts()
    {
        var _Response = new GetReceiptsResponse()
        {
            Receipts = [.. m_Context.Receipts]
        };

        return Results.Ok(_Response);
    }
}
