using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerms
{

    public class DeletePaymentTermHandler(SQLContext context) : IUseCaseHandler<DeletePaymentTermRequest>
    {
        public async Task<IResult> HandleAsync(DeletePaymentTermRequest request, CancellationToken cancellationToken)
        {
            var _PaymentTerm = await context.GetEntities<PaymentTerm>()
                .SingleAsync(pt => pt.PaymentTermID == request.PaymentTermID, cancellationToken);

            context.PaymentTerms.Remove(_PaymentTerm);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }

}
