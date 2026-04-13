using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerm
{

    public class DeletePaymentTermRequest : IUseCaseRequest
    {
        public long PaymentTermID { get; set; }
    }

}
