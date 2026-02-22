using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceResponse
{
    public Invoice Invoice { get; set; }

    public List<InvoiceItem> InvoiceItems { get; set; } = [];
}
