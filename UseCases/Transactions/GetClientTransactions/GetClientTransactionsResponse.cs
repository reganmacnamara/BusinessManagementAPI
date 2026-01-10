using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetClientTransactions
{

    public class GetClientTransactionsResponse
    {
        public List<Transaction> Transactions { get; set; }
    }
}

