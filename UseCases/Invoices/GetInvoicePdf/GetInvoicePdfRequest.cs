using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoicePdf;

public class GetInvoicePdfRequest : IUseCaseRequest
{
    public long InvoiceID { get; set; }
}
