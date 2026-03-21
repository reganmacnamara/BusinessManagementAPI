using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;

public class UpdateReceiptHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<UpdateReceiptRequest>
{
    public async Task<IResult> HandleAsync(UpdateReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = m_Context.GetEntities<Receipt>()
            .SingleOrDefault(r => r.ReceiptID == request.ReceiptID);

        if (_Receipt == null || request.ReceiptID == 0)
            return Results.NotFound("Receipt could not be found.");

        _Receipt = UpdateEntityFromRequest(_Receipt, request, ["ReceiptID"]);

        await m_Context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateReceiptResponse()
        {
            ReceiptID = _Receipt.ReceiptID
        };

        return Results.Ok(_Response);
    }
}
