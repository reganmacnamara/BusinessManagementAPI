using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.UseCases.Transactions.GetClientTransactions
{

    public class GetClientTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }
}

