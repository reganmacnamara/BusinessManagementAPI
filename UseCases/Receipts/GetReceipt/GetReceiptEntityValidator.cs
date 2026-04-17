using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetReceiptRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return EntityValidationResult.Failure(nameof(Receipt), request.ReceiptID);

        return EntityValidationResult.Success();
    }
}
