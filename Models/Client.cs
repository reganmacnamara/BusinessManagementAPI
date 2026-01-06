using System.ComponentModel.DataAnnotations;

namespace InvoiceAutomationAPI.Models
{

    public class Client
    {
        [Key]
        public long ClientID { get; set; }

        public string ClientName { get; set; } = string.Empty;
    }

}
