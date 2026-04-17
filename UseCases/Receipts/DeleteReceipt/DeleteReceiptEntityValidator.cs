using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteReceiptRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return EntityValidationResult.Failure(nameof(Receipt), request.ReceiptID);

        return EntityValidationResult.Success();
    }
}
