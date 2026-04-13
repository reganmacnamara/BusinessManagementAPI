using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceiptPdf;

public class GetReceiptPdfEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<GetReceiptPdfRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(GetReceiptPdfRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return (false, $"Receipt {request.ReceiptID} could not be found.");

        return (true, string.Empty);
    }
}
