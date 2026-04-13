using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerm
{

    public class GetPaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetPaymentTermRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(GetPaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return (false, $"Payment Term {request.PaymentTermID} could not be found.");

            return (true, string.Empty);
        }
    }

}
