using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.Infrastructure.Services.Allocations;

public class AllocationService : IAllocationService
{
    public async Task<Invoice> AllocateToInvoice(ReceiptItem allocation, Invoice invoice)
    {
        if (!invoice.Outstanding)
            throw new Exception("Cannot allocate to an Invoice that is already fully paid.");

        if ((invoice.OffsetValue + allocation.NetValue) > invoice.NetValue)
            throw new Exception("Cannot allocate more to Invoice than it's Net Value.");

        invoice.OffsetValue += allocation.NetValue;
        invoice.Outstanding = !(invoice.OffsetValue == invoice.NetValue);

        return invoice;
    }

    public async Task DeallocateFromInvoice(ReceiptItem allocation, Invoice invoice)
    {
        invoice.OffsetValue -= allocation.NetValue;
        invoice.Outstanding = !(invoice.OffsetValue == invoice.NetValue);
    }
}
