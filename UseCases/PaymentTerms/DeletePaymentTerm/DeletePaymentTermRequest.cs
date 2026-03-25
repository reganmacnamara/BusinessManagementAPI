using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerms
{

    public class DeletePaymentTermRequest : IUseCaseRequest
    {
        public long PaymentTermID { get; set; }
    }

}
