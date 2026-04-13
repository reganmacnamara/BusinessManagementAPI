using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpsertReceiptItem;

public class UpsertReceiptItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpsertReceiptItemRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(UpsertReceiptItemRequest request, CancellationToken cancellationToken)
    {
        if (request.ReceiptItemID != 0)
        {
            if (!existenceChecker.ValidateEntityExists<ReceiptItem>(request.ReceiptItemID))
                return (false, $"Receipt Item {request.ReceiptItemID} could not be found.");
        }
        else
        {
            if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
                return (false, $"Receipt {request.ReceiptID} could not be found.");

            if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
                return (false, $"Invoice {request.InvoiceID} could not be found.");
        }

        return (true, string.Empty);
    }
}
