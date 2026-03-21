using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptHandler(SQLContext context) : IUseCaseHandler<GetReceiptRequest>
{
    public async Task<IResult> HandleAsync(GetReceiptRequest request, CancellationToken cancellationToken)
    {
        var _Receipt = context.GetEntities<Receipt>()
            .AsNoTracking()
            .Include(r => r.Client)
            .Where(i => i.ReceiptID == request.ReceiptID)
            .SingleOrDefault();

        if (_Receipt is null)
            return Results.NotFound($"Receipt {request.ReceiptID} could not be found.");

        var _ReceiptItems = context.GetEntities<ReceiptItem>()
            .AsNoTracking()
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
