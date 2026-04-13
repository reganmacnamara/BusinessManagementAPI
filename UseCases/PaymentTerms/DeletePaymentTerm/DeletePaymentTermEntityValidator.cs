using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerm
{

    public class DeletePaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeletePaymentTermRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(DeletePaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return (false, $"Payment Term {request.PaymentTermID} could not be found.");

            return (true, string.Empty);
        }
    }

}
