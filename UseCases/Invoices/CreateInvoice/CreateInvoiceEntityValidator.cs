using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.CreateInvoice;

public class CreateInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<CreateInvoiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return EntityValidationResult.Failure(nameof(Client), request.ClientID);

        return EntityValidationResult.Success();
    }
}
