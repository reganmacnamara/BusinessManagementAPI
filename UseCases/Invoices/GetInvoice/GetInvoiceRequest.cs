using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceRequest : IUseCaseRequest
{
    public long InvoiceID { get; set; }
}
