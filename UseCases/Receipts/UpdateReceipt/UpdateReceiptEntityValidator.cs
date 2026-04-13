using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.UpdateReceipt;

public class UpdateReceiptEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<UpdateReceiptRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return (false, $"Receipt {request.ReceiptID} could not be found.");

        return (true, string.Empty);
    }
}
