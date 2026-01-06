using InvoiceAutomationAPI.Enums;

namespace InvoiceAutomationAPI.Models
{

    public class Invoice : Transaction
    {
        public TransactionType TransactionType => TransactionType.Invoice;

        public DateOnly DueDate { get; set; }
    }

}
