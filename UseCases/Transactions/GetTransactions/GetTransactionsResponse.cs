using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }

}
