namespace BusinessManagementAPI.Entities;

public class Receipt
{
    public long ReceiptID { get; set; }
    public string ReceiptRef { get; set; } = string.Empty;
    public DateTime? ReceiptDate { get; set; }
    public long ClientID { get; set; }
    public bool Outstanding { get; set; }
    public decimal GrossValue { get; set; }
    public decimal TaxValue { get; set; }
    public decimal NetValue { get; set; }
    public decimal OffsetValue { get; set; }


    //Navigation Properties

    public Client Client { get; set; }
    public List<ReceiptItem> ReceiptItems { get; set; }
}
