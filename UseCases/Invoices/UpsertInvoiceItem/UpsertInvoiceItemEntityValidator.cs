using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;

public class UpsertInvoiceItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpsertInvoiceItemRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpsertInvoiceItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return EntityValidationResult.Failure(nameof(Product), request.ProductID);

        if (request.InvoiceItemID != 0)
        {
            if (!existenceChecker.ValidateEntityExists<InvoiceItem>(request.InvoiceItemID))
                return EntityValidationResult.Failure(nameof(InvoiceItem), request.InvoiceItemID);
        }
        else
        {
            if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
                return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);
        }

        return EntityValidationResult.Success();
    }
}
