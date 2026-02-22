using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.Services;

public interface IAllocationService
{
    public Task<Invoice> AllocateToInvoice(ReceiptItem allocation, Invoice invoice);

    public Task DeallocateFromInvoice(ReceiptItem allocation, Invoice invoice);
}
