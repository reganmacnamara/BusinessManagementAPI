using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpsertInvoiceItem;

public class UpsertInvoiceItemEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpsertInvoiceItemRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(UpsertInvoiceItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return (false, $"Product {request.ProductID} could not be found.");

        if (request.InvoiceItemID != 0)
        {
            if (!existenceChecker.ValidateEntityExists<InvoiceItem>(request.InvoiceItemID))
                return (false, $"Invoice Item {request.InvoiceItemID} could not be found.");
        }
        else
        {
            if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
                return (false, $"Invoice {request.InvoiceID} could not be found.");
        }

        return (true, string.Empty);
    }
}
