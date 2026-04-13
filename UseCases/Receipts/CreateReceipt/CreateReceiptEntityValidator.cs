using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Receipts.CreateReceipt;

public class CreateReceiptEntityValidator(ExistenceChecker existenceChecker) : IUseCaseEntityValidator<CreateReceiptRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(CreateReceiptRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return (false, $"Client {request.ClientID} could not be found.");

        return (true, string.Empty);
    }
}
