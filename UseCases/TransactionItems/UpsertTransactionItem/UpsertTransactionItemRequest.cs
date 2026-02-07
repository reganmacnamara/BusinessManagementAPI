namespace BusinessManagementAPI.UseCases.TransactionItems.UpsertTransactionItem;

public class UpsertTransactionItemRequest
{
    public long TransactionItemID { get; set; }

    public long TransactionID { get; set; }

    public long ProductID { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerItem { get; set; }

    public decimal LineGross { get; set; }

    public decimal LineTax { get; set; }

    public decimal LineNet { get; set; }
}
