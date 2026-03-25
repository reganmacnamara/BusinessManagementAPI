using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.UpdatePaymentTerms
{

    public class UpdatePaymentTermRequest : IUseCaseRequest
    {
        public long PaymentTermID { get; set; }

        public required string PaymentTermName { get; set; }

        public int Value { get; set; }

        public int Unit { get; set; }

        public bool IsEndOf { get; set; }

        public bool IsStartingNext { get; set; }
    }

}
