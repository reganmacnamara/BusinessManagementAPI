using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }

}
