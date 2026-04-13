using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptEntityValidator(ExistenceChecker existenceChecker) : IUseCaseEntityValidator<GetReceiptRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(GetReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return (false, $"Receipt {request.ReceiptID} could not be found.");

        return (true, string.Empty);
    }
}
