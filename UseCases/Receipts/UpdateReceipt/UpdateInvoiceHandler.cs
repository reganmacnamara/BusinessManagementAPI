using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Receipts.UpdateReceipt;

public class UpdateReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> UpdateReceipt(UpdateReceiptRequest request)
    {
        var _Receipt = m_Context.Receipts.Find(request.ReceiptID);

        if (_Receipt == null || request.ReceiptID == 0)
            return Results.NotFound("Receipt could not be found.");

        _Receipt = UpdateEntityFromRequest(_Receipt, request, ["ReceiptID"]);

        await m_Context.SaveChangesAsync();

        var _Response = new UpdateReceiptResponse()
        {
            ReceiptID = _Receipt.ReceiptID
        };

        return Results.Ok(_Response);
    }
}
