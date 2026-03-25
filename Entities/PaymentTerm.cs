namespace MacsBusinessManagementAPI.Entities
{

    public class PaymentTerm
    {
        public long PaymentTermID { get; set; }

        public required string PaymentTermName { get; set; }

        public int Value { get; set; }

        public int Unit { get; set; }

        public bool IsEndOf { get; set; }

        public bool IsStartingNext { get; set; }
    }

}
