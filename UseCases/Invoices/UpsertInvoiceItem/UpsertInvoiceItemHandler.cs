using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem
{
    public class UpsertInvoiceItemHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> CreateInvoiceItem(UpsertInvoiceItemRequest request)
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

            _InvoiceItem.Invoice = _Invoice;
            _InvoiceItem.Product = _Product;

            m_Context.InvoiceItems.Add(_InvoiceItem);

            _ = await m_Context.SaveChangesAsync();

            var _Response = new UpsertInvoiceItemResponse()
            {
                InvoiceItemID = _InvoiceItem.InvoiceItemID
            };

            return Results.Created(string.Empty, _Response);
        }

        public async Task<IResult> UpdateInvoiceItem(UpsertInvoiceItemRequest request)
        {
            var _InvoiceItem = m_Context.GetEntities<InvoiceItem>()
                .Include(ii => ii.Invoice)
                .Include(ii => ii.Product)
                .Where(ii => ii.InvoiceItemID == request.InvoiceItemID)
                .SingleOrDefault();

            if (_InvoiceItem == null)
                return Results.NotFound("Invoice Item could not be found.");

            _InvoiceItem = UpdateEntityFromRequest(_InvoiceItem, request, ["InvoiceItemID"]);

            _ = await m_Context.SaveChangesAsync();

            var _Response = new UpsertInvoiceItemResponse()
            {
                InvoiceItemID = _InvoiceItem.InvoiceItemID
            };

            return Results.Ok(_Response);
        }
    }
}
