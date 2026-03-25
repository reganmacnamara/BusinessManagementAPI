using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.CreatePaymentTerms
{

    public class CreatePaymentTermRequest : IUseCaseRequest
    {
        public required string PaymentTermName { get; set; }

        public int Value { get; set; }

        public int Unit { get; set; }

        public bool IsEndOf { get; set; }

        public bool IsStartingNext { get; set; }
    }

}
