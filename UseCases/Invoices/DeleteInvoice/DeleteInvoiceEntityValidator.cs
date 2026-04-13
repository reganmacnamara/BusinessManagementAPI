using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteInvoiceRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return (false, $"Invoice {request.InvoiceID} could not be found.");

        return (true, string.Empty);
    }
}
