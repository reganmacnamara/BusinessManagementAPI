using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<CreateInvoiceRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return (false, $"Client {request.ClientID} could not be found.");

        return (true, string.Empty);
    }
}
