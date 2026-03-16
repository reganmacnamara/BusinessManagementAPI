using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceResponse
{
    public Invoice Invoice { get; set; } = default!;

    public List<InvoiceItem> InvoiceItems { get; set; } = [];
}
