namespace InvoiceAutomationAPI.UseCases.TransactionItems.CreateTransactionItem;

public class CreateTransactionItemRequest
{
    public long TransactionID { get; set; }

    public long ProductID { get; set; }

    public int Quantity { get; set; }

    public int PricePerItem { get; set; }

    public decimal LineGross { get; set; }

    public decimal LineTax { get; set; }

    public decimal LineNet { get; set; }
}
