using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceRequest : IUseCaseRequest
{
    public string InvoiceRef { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public long ClientID { get; set; }

    public decimal GrossValue { get; set; }

    public decimal TaxValue { get; set; }

    public decimal NetValue { get; set; }

    public decimal OffsetValue { get; set; }
}
