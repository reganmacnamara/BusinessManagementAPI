using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemRequest : IUseCaseRequest
{
    public long InvoiceItemID { get; set; }
}
