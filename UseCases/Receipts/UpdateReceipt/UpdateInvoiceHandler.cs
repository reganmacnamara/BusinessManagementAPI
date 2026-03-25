using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;

public class UpdateReceiptHandler(SQLContext context) : IUseCaseHandler<UpdateReceiptRequest>
{
    public async Task<IResult> HandleAsync(UpdateReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = await context.GetEntities<Receipt>()
            .SingleOrDefaultAsync(r => r.ReceiptID == request.ReceiptID, cancellationToken);

        if (_Receipt == null || request.ReceiptID == 0)
            return Results.NotFound("Receipt could not be found.");

        _Receipt.UpdateFromEntity(request, [nameof(Receipt.ReceiptID)]);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateReceiptResponse()
        {
            ReceiptID = _Receipt.ReceiptID
        };

        return Results.Ok(_Response);
    }
}
