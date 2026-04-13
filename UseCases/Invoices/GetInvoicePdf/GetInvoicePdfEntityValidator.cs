using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoicePdf;

public class GetInvoicePdfEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<GetInvoicePdfRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(GetInvoicePdfRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return (false, $"Invoice {request.InvoiceID} could not be found.");

        return (true, string.Empty);
    }
}
