namespace BusinessManagementAPI.Entities;

public class ReceiptItem
{
    public long ReceiptItemID { get; set; }
    public long ReceiptID { get; set; }
    public long InvoiceID { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxValue { get; set; }
    public decimal NetValue { get; set; }

    //Navigation Properties

    public Invoice Invoice { get; set; }
    public Receipt Receipt { get; set; }
}
