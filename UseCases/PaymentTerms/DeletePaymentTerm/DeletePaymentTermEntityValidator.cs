using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerm
{

    public class DeletePaymentTermEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeletePaymentTermRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(DeletePaymentTermRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID))
                return EntityValidationResult.Failure(nameof(PaymentTerm), request.PaymentTermID);

            return EntityValidationResult.Success();
        }
    }

}
