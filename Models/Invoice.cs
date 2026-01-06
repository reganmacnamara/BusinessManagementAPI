namespace InvoiceAutomationAPI.Models
{

    public class Invoice : Transaction
    {
        public string TransactionType { get; init; } = "INV";

        public DateOnly DueDate { get; set; }
    }

}
