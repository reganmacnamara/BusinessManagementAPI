namespace MacsBusinessManagementAPI.Entities
{

    public class Company
    {
        public long CompanyID { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyABN { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string AddressLine2 { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        //Navigational Properties

        public List<Account> Accounts { get; set; } = [];

        public List<Client> Clients { get; set; } = [];

        public List<Invoice> Invoices { get; set; } = [];

        public List<PaymentTerm> PaymentTerms { get; set; } = [];

        public List<Product> Products { get; set; } = [];

        public List<Receipt> Receipts { get; set; } = [];
    }

}
