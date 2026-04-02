namespace MacsBusinessManagementAPI.Entities
{

    public class CompanySettings
    {
        public long CompanySettingsID { get; set; }

        public string InvoicePrefix { get; set; } = string.Empty;

        public long NextInvoiceNumber { get; set; }

        public bool AutoIncrementInvoice { get; set; }

        public string ReceiptPrefix { get; set; } = string.Empty;

        public long NextReceiptNumber { get; set; }

        public bool AutoIncrementReceipt { get; set; }
    }

}
