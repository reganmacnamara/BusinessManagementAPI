using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem
{
    public class UpsertInvoiceItemHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<UpsertInvoiceItemRequest>
    {
        public async Task<IResult> HandleAsync(UpsertInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            if (request.InvoiceItemID != 0)
            {
                var _InvoiceItem = await context.GetEntities<InvoiceItem>()
                    .Include(ii => ii.Invoice)
                    .Include(ii => ii.Product)
                    .Where(ii => ii.InvoiceItemID == request.InvoiceItemID)
                    .SingleOrDefaultAsync(cancellationToken);

                if (_InvoiceItem == null)
                    return Results.NotFound("Invoice Item could not be found.");

                var _OldQuantity = _InvoiceItem.Quantity;
                var _OldProductID = _InvoiceItem.ProductID;
                var _OldProduct = _InvoiceItem.Product;

                _InvoiceItem.UpdateFromEntity(request, ["InvoiceItemID"]);

                Product? _Product;
                if (_InvoiceItem.ProductID != _OldProductID)
                {
                    _Product = await context.GetEntities<Product>()
                        .SingleOrDefaultAsync(p => p.ProductID == _InvoiceItem.ProductID, cancellationToken);

                    if (_Product == null)
                        return Results.NotFound("Product could not be found.");
                }
                else
                    _Product = _OldProduct;

                _OldProduct.QuantityOnHand += (long)_OldQuantity;

                if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                    return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;

                _ = await context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertInvoiceItemResponse()
                {
                    InvoiceItemID = _InvoiceItem.InvoiceItemID
                };

                return Results.Ok(_Response);
            }
            else
            {
                var _InvoiceItem = mapper.Map<InvoiceItem>(request);

                var _Invoice = await context.GetEntities<Invoice>()
                    .SingleOrDefaultAsync(i => i.InvoiceID == request.InvoiceID, cancellationToken);

                if (_Invoice == null)
                    return Results.NotFound("Invoice could not be found.");

                var _Product = await context.GetEntities<Product>()
                    .SingleOrDefaultAsync(p => p.ProductID == request.ProductID, cancellationToken);

                if (_Product == null)
                    return Results.NotFound("Product could not be found.");

                if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                    return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                _InvoiceItem.Invoice = _Invoice;
                _InvoiceItem.Product = _Product;

                _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;

                context.InvoiceItems.Add(_InvoiceItem);

                _ = await context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertInvoiceItemResponse()
                {
                    InvoiceItemID = _InvoiceItem.InvoiceItemID
                };

                return Results.Created(string.Empty, _Response);
            }
        }
    }
}
