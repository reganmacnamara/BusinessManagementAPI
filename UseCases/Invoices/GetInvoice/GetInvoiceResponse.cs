using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceResponse
{
    public Invoice Invoice { get; set; } = default!;

    public List<InvoiceItem> InvoiceItems { get; set; } = [];
}
