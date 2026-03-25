using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerm
{

    public class GetPaymentTermRequest : IUseCaseRequest
    {
        public long PaymentTermID { get; set; }
    }

}
