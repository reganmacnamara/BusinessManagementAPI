using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceEntityValidator(ExistenceChecker existenceChecker) : IUseCaseEntityValidator<GetInvoiceRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(GetInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return (false, $"Invoice {request.InvoiceID} could not be found.");

        return (true, string.Empty);
    }
}
