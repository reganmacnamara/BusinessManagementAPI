using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;

public class CreateReceiptEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<CreateReceiptRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return EntityValidationResult.Failure(nameof(Client), request.ClientID);

        return EntityValidationResult.Success();
    }
}
