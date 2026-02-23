using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.Services;

public class AllocationService : IAllocationService
{
    public async Task<Invoice> AllocateToInvoice(ReceiptItem allocation, Invoice invoice)
    {
        if (!invoice.Outstanding && allocation.NetValue <= invoice.NetValue)
        {
            if ((invoice.OffsetValue + allocation.NetValue) > invoice.NetValue)
                throw new Exception("Cannot allocate more to Invoice than its Net Value.");

            invoice.OffsetValue += allocation.NetValue;
            invoice.Outstanding = invoice.OffsetValue == invoice.NetValue;
        }
        return invoice;
    }

    public async Task DeallocateFromInvoice(ReceiptItem allocation, Invoice invoice)
    {
        throw new NotImplementedException();
    }
}
