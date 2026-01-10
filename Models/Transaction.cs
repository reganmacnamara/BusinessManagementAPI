namespace InvoiceAutomationAPI.Models
{

    public class Transaction
    {
        public long TransactionID { get; set; }

        public string TransactionRef { get; set; } = string.Empty;

        public DateTime? TransactionDate { get; set; }

        public string TransactionType { get; set; }

        public DateTime? DueDate { get; set; }

        public long ClientID { get; set; }

        public decimal GrossValue { get; set; }

        public decimal TaxValue { get; set; }

        public decimal NetValue { get; set; }

        public decimal OffsetValue { get; set; }

        public Client Client { get; set; }
    }

}
