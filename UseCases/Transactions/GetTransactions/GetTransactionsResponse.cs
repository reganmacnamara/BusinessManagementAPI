using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetTransactions
{

    public class GetTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }

}
