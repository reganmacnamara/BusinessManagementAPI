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
            .Include(r => r.Client)
            .Where(i => i.ReceiptID == request.ReceiptID)
            .SingleOrDefault();

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        var _ReceiptItems = m_Context.ReceiptItems
            .Include(ri => ri.Invoice)
            .Where(ri => ri.ReceiptID == request.ReceiptID)
            .ToList();

        var _Response = new GetReceiptResponse()
        {
            Receipt = _Receipt,
            ReceiptItems = _ReceiptItems
        };

        return Results.Ok(_Response);
    }
}
