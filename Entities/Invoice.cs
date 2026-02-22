namespace BusinessManagementAPI.Entities;

public class Invoice
{
    public long TransactionID { get; set; }

    public string TransactionRef { get; set; } = string.Empty;

    public DateTime? TransactionDate { get; set; }

    public string TransactionType { get; set; } = "INV";

    public DateTime? DueDate { get; set; }

    public long ClientID { get; set; }

    public bool Outstanding { get; set; }

    public decimal GrossValue { get; set; }

    public decimal TaxValue { get; set; }

    public decimal NetValue { get; set; }

    public decimal OffsetValue { get; set; }


    //Navigation Properties

    public Client Client { get; set; }
    public List<InvoiceItem> InvoiceItems { get; set; } = [];
}
