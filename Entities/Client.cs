using System.ComponentModel.DataAnnotations;

namespace MacsBusinessManagementAPI.Entities
{

    public class Client
    {
        [Key]
        public long ClientID { get; set; }

        public string ClientName { get; set; } = string.Empty;

        public string ClientEmail { get; set; } = string.Empty;

        public string ClientPhone { get; set; } = string.Empty;

        public string ClientMobile { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string AddressLine2 { get; set; } = string.Empty;

        public string PostCode { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public long? PaymentTermID { get; set; }

        //Navigational Properties

        public PaymentTerm? PaymentTerm { get; set; }
    }

}
