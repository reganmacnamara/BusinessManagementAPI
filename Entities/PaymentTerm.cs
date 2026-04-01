namespace MacsBusinessManagementAPI.Entities
{

    public class PaymentTerm
    {
        public long PaymentTermID { get; set; }

        public long CompanyID { get; set; }

        public required string PaymentTermName { get; set; }

        public int Days { get; set; }

        public int Months { get; set; }

        public bool EndOfMonth { get; set; }

        public int? DayOfMonth { get; set; }

        public bool OffsetFirst { get; set; }

        //Navigational Properties

        public Company Company { get; set; } = default!;
    }

}
