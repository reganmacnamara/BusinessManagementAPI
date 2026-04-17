using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerm
{

    public class GetPaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetPaymentTermRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(GetPaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return EntityValidationResult.Failure(nameof(PaymentTerm), request.PaymentTermID);

            return EntityValidationResult.Success();
        }
    }

}
