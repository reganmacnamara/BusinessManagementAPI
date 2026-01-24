using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.TransactionItems.GetTransactionItems;

public class GetTransactionItemsResponse
{
    public List<TransactionItem> TransactionItems { get; set; }
}
