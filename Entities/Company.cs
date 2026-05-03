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

        public long CompanySettingsID { get; set; }

        //Navigational Properties

        public ICollection<Account> Accounts { get; set; } = [];

        public ICollection<Client> Clients { get; set; } = [];

        public CompanySettings CompanySettings { get; set; }

        public ICollection<Invoice> Invoices { get; set; } = [];

        public ICollection<PaymentTerm> PaymentTerms { get; set; } = [];

        public ICollection<Product> Products { get; set; } = [];

        public ICollection<Receipt> Receipts { get; set; } = [];

        public ICollection<Service> Services { get; set; } = [];
    }

}
