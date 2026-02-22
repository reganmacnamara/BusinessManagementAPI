namespace BusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;

public class UpsertReceiptItemRequest
{
    public long ReceiptItemID { get; set; }
    public long ReceiptID { get; set; }
    public long InvoiceID { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxValue { get; set; }
    public decimal NetValue { get; set; }
}
