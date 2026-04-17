using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;

public class GetReceiptPdfEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetReceiptPdfRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetReceiptPdfRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return EntityValidationResult.Failure(nameof(Receipt), request.ReceiptID);

        return EntityValidationResult.Success();
    }
}
