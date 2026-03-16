using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.Services;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemHandler(IAllocationService allocationService, IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
{
    public async Task<IResult> DeleteReceiptItem(DeleteReceiptItemRequest request)
    {
        var _ReceiptItem = m_Context.GetEntities<ReceiptItem>()
            .Include(ri => ri.Invoice)
            .Include(ri => ri.Receipt)
            .SingleOrDefault(ri => ri.ReceiptItemID == request.ReceiptItemID);

        if (_ReceiptItem is null)
            return Results.NotFound("Receipt Item not found.");

        await allocationService.DeallocateFromInvoice(_ReceiptItem, _ReceiptItem.Invoice);

        _ReceiptItem.Receipt.GrossValue -= _ReceiptItem.GrossValue;
        _ReceiptItem.Receipt.TaxValue -= _ReceiptItem.TaxValue;
        _ReceiptItem.Receipt.NetValue -= _ReceiptItem.NetValue;

        m_Context.ReceiptItems.Remove(_ReceiptItem);

        _ = await m_Context.SaveChangesAsync();

        return Results.NoContent();
    }
}
