namespace MacsBusinessManagementAPI.Entities;

public class Invoice
{
    public long InvoiceID { get; set; }

    public long CompanyID { get; set; }

    public string InvoiceRef { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public DateTime DueDate { get; set; }

    public long ClientID { get; set; }

    public bool Outstanding { get; set; }

    public decimal GrossValue { get; set; }

    public decimal TaxValue { get; set; }

    public decimal NetValue { get; set; }

    public decimal OffsetValue { get; set; }

    public long PaymentTermID { get; set; }

    //Navigation Properties

    public Client Client { get; set; } = default!;

    public Company Company { get; set; } = default!;

    public PaymentTerm PaymentTerm { get; set; } = default!;
}
