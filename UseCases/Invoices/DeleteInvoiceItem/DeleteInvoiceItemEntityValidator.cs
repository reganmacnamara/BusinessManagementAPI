using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.DeleteInvoiceItem;

public class DeleteInvoiceItemEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<DeleteInvoiceItemRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteInvoiceItemRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<InvoiceItem>(request.InvoiceItemID))
            return (false, $"Invoice Item {request.InvoiceItemID} could not be found.");

        return (true, string.Empty);
    }
}
