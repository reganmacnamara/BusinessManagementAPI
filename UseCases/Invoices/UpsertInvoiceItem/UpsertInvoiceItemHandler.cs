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
                    .Include(ii => ii.Product)
                    .Include(ii => ii.Service)
                    .Where(ii => ii.InvoiceItemID == request.InvoiceItemID)
                    .SingleAsync(cancellationToken);

                var _OldQuantity = _InvoiceItem.Quantity;
                var _OldProductID = _InvoiceItem.ProductID;
                var _OldProduct = _InvoiceItem.Product;

                _InvoiceItem.UpdateFromEntity(request, [nameof(InvoiceItem.InvoiceItemID)]);

                // Product stock handling (only for product-backed items)
                if (_OldProduct is not null)
                    _OldProduct.QuantityOnHand += (long)_OldQuantity;

                if (_InvoiceItem.ProductID.HasValue)
                {
                    Product? _Product;
                    if (_InvoiceItem.ProductID != _OldProductID)
                    {
                        _Product = await context.GetEntities<Product>()
                            .SingleAsync(p => p.ProductID == _InvoiceItem.ProductID, cancellationToken);
                    }
                    else
                        _Product = _OldProduct;

                    if (_Product is not null)
                    {
                        if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                            return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                        _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;
                    }
                }

                // Service handling
                if (_InvoiceItem.ServiceID.HasValue)
                {
                    var _Service = await context.GetEntities<Service>()
                        .SingleAsync(s => s.ServiceID == _InvoiceItem.ServiceID, cancellationToken);

                    _InvoiceItem.Service = _Service;
                }

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

                // Product-backed line item
                if (request.ProductID.HasValue)
                {
                    var _Product = await context.GetEntities<Product>()
                        .SingleAsync(p => p.ProductID == request.ProductID, cancellationToken);

                    if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                        return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                    _InvoiceItem.Product = _Product;
                    _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;
                }

                // Service-backed line item
                if (request.ServiceID.HasValue)
                {
                    var _Service = await context.GetEntities<Service>()
                        .SingleAsync(s => s.ServiceID == request.ServiceID, cancellationToken);

                    _InvoiceItem.Service = _Service;
                }

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
