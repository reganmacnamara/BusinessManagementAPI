using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.UpdatePaymentTerms
{

    public class UpdatePaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdatePaymentTermRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(UpdatePaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return (false, $"Payment Term {request.PaymentTermID} could not be found.");

            return (true, string.Empty);
        }
    }

}
