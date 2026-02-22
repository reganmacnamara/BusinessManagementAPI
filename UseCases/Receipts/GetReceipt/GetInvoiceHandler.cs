using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> GetReceipt(GetReceiptRequest request)
    {
        var _Receipt = m_Context.GetEntities<Receipt>()
            .Include(i => i.ReceiptItems)
            .Where(i => i.ReceiptID == request.ReceiptID)
            .SingleOrDefault();

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        var _Response = new GetReceiptResponse()
        {
            Receipt = _Receipt,
            ReceiptItems = _Receipt.ReceiptItems
        };

        return Results.Ok(_Response);
    }
}
