using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.Services.Allocations;
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
                var _Receipt = context.GetEntities<Receipt>()
                    .SingleOrDefault(r => r.ReceiptID == request.ReceiptID);

                if (_Receipt == null)
                    return Results.NotFound("Receipt could not be found.");

                var _Invoice = context.GetEntities<Invoice>()
                    .SingleOrDefault(i => i.InvoiceID == request.InvoiceID);

                if (_Invoice == null)
                    return Results.NotFound("Invoice could not be found.");

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
                var _ReceiptItem = context.GetEntities<ReceiptItem>()
                    .Include(ii => ii.Receipt)
                    .Include(ii => ii.Invoice)
                    .Where(ii => ii.ReceiptItemID == request.ReceiptItemID)
                    .SingleOrDefault();

                if (_ReceiptItem == null)
                    return Results.NotFound("Receipt Item could not be found.");

                await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

                _ReceiptItem.UpdateFromEntity(request, ["ReceiptItemID"]);

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
