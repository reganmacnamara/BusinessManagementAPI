using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.CreatePaymentTerms
{

    public class CreatePaymentTermRequest : IUseCaseRequest
    {
        public required string PaymentTermName { get; set; }

        public int Days { get; set; }

        public int Months { get; set; }

        public bool EndOfMonth { get; set; }

        public int? DayOfMonth { get; set; }

        public bool OffsetFirst { get; set; }
    }

}
