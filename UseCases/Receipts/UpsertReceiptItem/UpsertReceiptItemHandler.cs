using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Base;
using BusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem
{
    public class UpsertReceiptItemHandler(IAllocationService allocationService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> CreateReceiptItem(UpsertReceiptItemRequest request)
        {
            var _ReceiptItem = m_Mapper.Map<ReceiptItem>(request);
            var _Receipt = await m_Context.Receipts.FindAsync(request.ReceiptID);

            if (_Receipt == null)
                return Results.NotFound("Receipt could not be found.");

            var _Invoice = await m_Context.Invoices.FindAsync(request.InvoiceID);

            if (_Invoice == null)
                return Results.NotFound("Invoice could not be found.");

            _ReceiptItem.Receipt = _Receipt;
            _ReceiptItem.Invoice = _Invoice;

            _Invoice = await allocationService.AllocateToInvoice(_ReceiptItem, _Invoice);

            m_Context.ReceiptItems.Add(_ReceiptItem);
            _Receipt.ReceiptItems.Add(_ReceiptItem);

            _ = await m_Context.SaveChangesAsync();

            var _Response = new UpsertReceiptItemResponse()
            {
                ReceiptItemID = _ReceiptItem.ReceiptItemID
            };

            return Results.Created(string.Empty, _Response);
        }

        public async Task<IResult> UpdateReceiptItem(UpsertReceiptItemRequest request)
        {
            var _ReceiptItem = m_Context.GetEntities<ReceiptItem>()
                .Include(ii => ii.Receipt)
                .Include(ii => ii.Invoice)
                .Where(ii => ii.ReceiptItemID == request.ReceiptItemID)
                .SingleOrDefault();

            if (_ReceiptItem == null)
                return Results.NotFound("Receipt Item could not be found.");

            _ReceiptItem = UpdateEntityFromRequest(_ReceiptItem, request, ["ReceiptItemID"]);

            _ReceiptItem.Invoice = await allocationService.AllocateToInvoice(_ReceiptItem, _ReceiptItem.Invoice);

            _ = await m_Context.SaveChangesAsync();

            var _Response = new UpsertReceiptItemResponse()
            {
                ReceiptItemID = _ReceiptItem.ReceiptItemID
            };

            return Results.Created(string.Empty, _Response);
        }
    }
}
