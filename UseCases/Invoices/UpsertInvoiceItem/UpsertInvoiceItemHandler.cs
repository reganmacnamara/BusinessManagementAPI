using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem
{
    public class UpsertInvoiceItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<UpsertInvoiceItemRequest>
    {
        public async Task<IResult> HandleAsync(UpsertInvoiceItemRequest request, CancellationToken cancellationToken)
        {
            if (request.InvoiceItemID != 0)
            {
                var _InvoiceItem = m_Context.GetEntities<InvoiceItem>()
                    .Include(ii => ii.Invoice)
                    .Include(ii => ii.Product)
                    .Where(ii => ii.InvoiceItemID == request.InvoiceItemID)
                    .SingleOrDefault();

                if (_InvoiceItem == null)
                    return Results.NotFound("Invoice Item could not be found.");

                var _OldQuantity = _InvoiceItem.Quantity;
                var _OldProductID = _InvoiceItem.ProductID;
                var _OldProduct = _InvoiceItem.Product;

                _InvoiceItem = UpdateEntityFromRequest(_InvoiceItem, request, ["InvoiceItemID"]);

                Product? _Product;
                if (_InvoiceItem.ProductID != _OldProductID)
                {
                    _Product = m_Context.GetEntities<Product>()
                        .SingleOrDefault(p => p.ProductID == _InvoiceItem.ProductID);

                    if (_Product == null)
                        return Results.NotFound("Product could not be found.");
                }
                else
                {
                    _Product = _OldProduct;
                }

                _OldProduct.QuantityOnHand += (long)_OldQuantity;

                if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                    return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;

                _ = await m_Context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertInvoiceItemResponse()
                {
                    InvoiceItemID = _InvoiceItem.InvoiceItemID
                };

                return Results.Ok(_Response);
            }
            else
            {
                var _InvoiceItem = m_Mapper.Map<InvoiceItem>(request);
                var _Invoice = m_Context.GetEntities<Invoice>()
                    .SingleOrDefault(i => i.InvoiceID == request.InvoiceID);

                if (_Invoice == null)
                    return Results.NotFound("Invoice could not be found.");

                var _Product = m_Context.GetEntities<Product>()
                    .SingleOrDefault(p => p.ProductID == request.ProductID);

                if (_Product == null)
                    return Results.NotFound("Product could not be found.");

                if ((decimal)_Product.QuantityOnHand < _InvoiceItem.Quantity)
                    return Results.Conflict($"Insufficient stock for '{_Product.ProductName}'. Available: {_Product.QuantityOnHand}.");

                _InvoiceItem.Invoice = _Invoice;
                _InvoiceItem.Product = _Product;

                _Product.QuantityOnHand -= (long)_InvoiceItem.Quantity;

                m_Context.InvoiceItems.Add(_InvoiceItem);

                _ = await m_Context.SaveChangesAsync(cancellationToken);

                var _Response = new UpsertInvoiceItemResponse()
                {
                    InvoiceItemID = _InvoiceItem.InvoiceItemID
                };

                return Results.Created(string.Empty, _Response);
            }
        }
    }
}
