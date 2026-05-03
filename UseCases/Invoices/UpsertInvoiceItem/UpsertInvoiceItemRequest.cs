using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;

public class UpsertInvoiceItemRequest : IUseCaseRequest
{
    public long InvoiceItemID { get; set; }
    public long InvoiceID { get; set; }
    public long? ProductID { get; set; }
    public long? ServiceID { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxValue { get; set; }
    public decimal NetValue { get; set; }
}
