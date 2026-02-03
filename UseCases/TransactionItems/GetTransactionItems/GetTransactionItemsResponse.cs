using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.TransactionItems.GetTransactionItems;

public class GetTransactionItemsResponse
{
    public List<TransactionItem> TransactionItems { get; set; }
}
