using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Transactions.CreateTransaction
{

    public class CreateTransactionRequest
    {
        public DateTime TransactionDate { get; init; } = DateTime.Now;

        public string TransactionType { get; init; }

        public DateOnly DueDate { get; set; }

        public Client Client { get; set; } = null!;

        public decimal GrossValue { get; set; }

        public decimal TaxValue { get; set; }

        public decimal NetValue { get; set; }

        public decimal OffsetValue { get; set; }
    }

}
