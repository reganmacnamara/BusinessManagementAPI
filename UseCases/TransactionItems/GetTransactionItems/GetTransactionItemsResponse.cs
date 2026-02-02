using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;

public class GetTransactionItemsResponse
{
    public List<TransactionItem> TransactionItems { get; set; }
}
