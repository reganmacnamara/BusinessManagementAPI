using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;

public class UpdateReceiptEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateReceiptRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpdateReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return EntityValidationResult.Failure(nameof(Receipt), request.ReceiptID);

        return EntityValidationResult.Success();
    }
}
