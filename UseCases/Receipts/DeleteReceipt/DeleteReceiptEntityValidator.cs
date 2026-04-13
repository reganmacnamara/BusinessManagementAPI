using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.DeleteReceipt;

public class DeleteReceiptEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<DeleteReceiptRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Receipt>(request.ReceiptID))
            return (false, $"Receipt {request.ReceiptID} could not be found.");

        return (true, string.Empty);
    }
}
