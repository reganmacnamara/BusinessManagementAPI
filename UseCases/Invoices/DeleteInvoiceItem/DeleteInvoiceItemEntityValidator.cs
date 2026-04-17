using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteInvoiceItemRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteInvoiceItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<InvoiceItem>(request.InvoiceItemID))
            return EntityValidationResult.Failure(nameof(InvoiceItem), request.InvoiceItemID);

        return EntityValidationResult.Success();
    }
}
