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

                var _Receipt = await context.GetEntities<Receipt>()
                    .SingleAsync(r => r.ReceiptID == request.ReceiptID, cancellationToken);

                var _Invoice = await context.GetEntities<Invoice>()
                    .SingleAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

                _ReceiptItem.Receipt = _Receipt;
                _ReceiptItem.Invoice = _Invoice;

                try
                {
                    _Invoice = await allocationService.AllocateToInvoice(_ReceiptItem, _Invoice);
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
                    .Include(ii => ii.Receipt)
                    .Include(ii => ii.Invoice)
                    .SingleAsync(ii => ii.ReceiptItemID == request.ReceiptItemID, cancellationToken);

                await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

                _ReceiptItem.UpdateFromEntity(request, [nameof(ReceiptItem.ReceiptItemID)]);

                try
                {
                    _ReceiptItem.Invoice = await allocationService.AllocateToInvoice(_ReceiptItem, _ReceiptItem.Invoice);
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
