using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateInvoiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return EntityValidationResult.Failure(nameof(Client), request.ClientID);

        if (request.PaymentTermID.HasValue && !existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID.Value))
            return EntityValidationResult.Failure(nameof(PaymentTerm), request.PaymentTermID.Value);

        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return EntityValidationResult.Failure(nameof(Invoice), request.InvoiceID);

        return EntityValidationResult.Success();
    }
}
