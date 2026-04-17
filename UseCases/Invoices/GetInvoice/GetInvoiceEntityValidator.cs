using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.GetInvoice;

public class GetInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetInvoiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);

        return EntityValidationResult.Success();
    }
}
