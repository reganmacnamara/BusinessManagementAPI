using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Transactions.GetTransaction;

public class GetTransactionResponse
{
    public Transaction Transaction { get; set; } = default!;

    public List<TransactionItem> TransactionItems { get; set; } = [];
}
