using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteReceiptItemRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteReceiptItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<ReceiptItem>(request.ReceiptItemID))
            return EntityValidationResult.Failure(nameof(ReceiptItem), request.ReceiptItemID);

        return EntityValidationResult.Success();
    }
}
