using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Infrastructure.Services.Allocations;

public interface IAllocationService
{
    public Task<Invoice> AllocateToInvoice(ReceiptItem allocation, Invoice invoice);

    public Task DeallocateFromInvoice(ReceiptItem allocation, Invoice invoice);
}
