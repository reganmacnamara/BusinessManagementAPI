using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoicePdf;

public class GetInvoicePdfEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetInvoicePdfRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetInvoicePdfRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);

        return EntityValidationResult.Success();
    }
}
