using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.Infrastructure.Services.Allocations;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem
{
    public class UpsertReceiptItemHandler(IAllocationService allocationService, IMapper mapper, SQLContext context) : IUseCaseHandler<UpsertReceiptItemRequest>
    {
        public async Task<IResult> HandleAsync(UpsertReceiptItemRequest request, CancellationToken cancellationToken)
        {
            if (request.ReceiptItemID == 0)
            {
                var _ReceiptItem = mapper.Map<ReceiptItem>(request);

                var _Invoice = await context.GetEntities<Invoice>()
                    .SingleAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

                try
                {
                    await allocationService.AllocateToInvoice(_ReceiptItem, _Invoice);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex.Message);
                }

                context.ReceiptItems.Add(_ReceiptItem);

                _ = await context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertReceiptItemResponse()
                {
                    ReceiptItemID = _ReceiptItem.ReceiptItemID
                };

                return Results.Created(string.Empty, _Response);
            }
            else
            {
                var _ReceiptItem = await context.GetEntities<ReceiptItem>()
                    .Include(ri => ri.Invoice)
                    .SingleAsync(ii => ii.ReceiptItemID == request.ReceiptItemID, cancellationToken);

                var _OldInvoice = _ReceiptItem.Invoice;

                await allocationService.DeallocateFromInvoice(_ReceiptItem, _OldInvoice);

                _ReceiptItem.UpdateFromEntity(request, [nameof(ReceiptItem.ReceiptItemID)]);

                var _Invoice = _ReceiptItem.InvoiceID == _OldInvoice.InvoiceID
                    ? _OldInvoice
                    : await context.GetEntities<Invoice>()
                        .SingleAsync(i => i.InvoiceID == _ReceiptItem.InvoiceID, cancellationToken);

                try
                {
                    await allocationService.AllocateToInvoice(_ReceiptItem, _Invoice);
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex.Message);
                }

                _ = await context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertReceiptItemResponse()
                {
                    ReceiptItemID = _ReceiptItem.ReceiptItemID
                };

                return Results.Ok(_Response);
            }
        }
    }
}
