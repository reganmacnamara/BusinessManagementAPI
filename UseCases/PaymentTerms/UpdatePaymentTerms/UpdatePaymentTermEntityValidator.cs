using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.UpdatePaymentTerms
{

    public class UpdatePaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdatePaymentTermRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(UpdatePaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return EntityValidationResult.Failure(nameof(PaymentTerm), request.PaymentTermID);

            return EntityValidationResult.Success();
        }
    }

}
