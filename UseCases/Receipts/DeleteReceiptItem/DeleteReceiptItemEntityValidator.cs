using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceiptItem;

public class DeleteReceiptItemEntityValidator(ExistenceChecker existenceChecker) : IUseCaseEntityValidator<DeleteReceiptItemRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteReceiptItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<ReceiptItem>(request.ReceiptItemID))
            return (false, $"Receipt Item {request.ReceiptItemID} could not be found.");

        return (true, string.Empty);
    }
}
