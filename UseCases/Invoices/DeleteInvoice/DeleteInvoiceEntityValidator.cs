using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoice;

public class DeleteInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteInvoiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);

        return EntityValidationResult.Success();
    }
}
