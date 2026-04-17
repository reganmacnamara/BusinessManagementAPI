using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;

public class UpsertReceiptItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpsertReceiptItemRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpsertReceiptItemRequest request, CancellationToken cancellationToken)
    {
        if (request.ReceiptItemID != 0)
        {
            if (!existenceChecker.ValidateEntityExists<ReceiptItem>(request.ReceiptItemID))
                return EntityValidationResult.Failure(nameof(ReceiptItem), request.ReceiptItemID);
        }
        else
        {
            if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
                return EntityValidationResult.Failure(nameof(Receipt), request.ReceiptID);

            if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
                return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);
        }

        return EntityValidationResult.Success();
    }
}
