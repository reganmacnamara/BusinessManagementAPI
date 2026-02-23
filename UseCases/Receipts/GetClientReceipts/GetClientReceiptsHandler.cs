using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.GetClientReceipts;

public class GetClientReceiptsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetClientReceipts(GetClientReceiptsRequest request)
    {
        var _Receipts = m_Context.GetEntities<Receipt>()
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
