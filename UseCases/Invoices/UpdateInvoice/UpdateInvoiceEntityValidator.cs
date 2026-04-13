using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Invoices.UpdateInvoice;

public class UpdateInvoiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateInvoiceRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
            return (false, $"Client {request.ClientID} could not be found.");

        if (request.PaymentTermID.HasValue && !existenceChecker.ValidateEntityExists<PaymentTerm>(request.PaymentTermID.Value))
            return (false, $"Payment Term {request.PaymentTermID} could not be found.");

        if (!existenceChecker.ValidateEntityExists<Invoice>(request.InvoiceID))
            return (false, $"Invoice {request.InvoiceID} could not be found.");

        return (true, string.Empty);
    }
}
