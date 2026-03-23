using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceRequest : IUseCaseRequest
{
    public long InvoiceID { get; set; }
}
