using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem
{
    public class UpsertReceiptItemHandler(IAllocationService allocationService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> CreateReceiptItem(UpsertReceiptItemRequest request)
        {
            var _ReceiptItem = m_Mapper.Map<ReceiptItem>(request);
            var _Receipt = m_Context.GetEntities<Receipt>()
                .SingleOrDefault(r => r.ReceiptID == request.ReceiptID);

            if (_Receipt == null)
                return Results.NotFound("Receipt could not be found.");

            var _Invoice = m_Context.GetEntities<Invoice>()
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

            _Receipt.GrossValue += _ReceiptItem.GrossValue;
            _Receipt.TaxValue += _ReceiptItem.TaxValue;
            _Receipt.NetValue += _ReceiptItem.NetValue;

            m_Context.ReceiptItems.Add(_ReceiptItem);

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

            await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

            _ReceiptItem.Receipt.GrossValue -= _ReceiptItem.GrossValue;
            _ReceiptItem.Receipt.TaxValue -= _ReceiptItem.TaxValue;
            _ReceiptItem.Receipt.NetValue -= _ReceiptItem.NetValue;

            _ReceiptItem = UpdateEntityFromRequest(_ReceiptItem, request, ["ReceiptItemID"]);

            try
            {
                _ReceiptItem.Invoice = await allocationService.AllocateToInvoice(_ReceiptItem, _ReceiptItem.Invoice);
            }
            catch (Exception ex)
            {
                return Results.Conflict(ex.Message);
            }

            _ReceiptItem.Receipt.GrossValue += _ReceiptItem.GrossValue;
            _ReceiptItem.Receipt.TaxValue += _ReceiptItem.TaxValue;
            _ReceiptItem.Receipt.NetValue += _ReceiptItem.NetValue;

            _ = await m_Context.SaveChangesAsync();

            var _Response = new UpsertReceiptItemResponse()
            {
                ReceiptItemID = _ReceiptItem.ReceiptItemID
            };

            return Results.Ok(_Response);
        }
    }
}
