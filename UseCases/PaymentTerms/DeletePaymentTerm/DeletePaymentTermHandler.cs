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
                .SingleOrDefaultAsync(pt => pt.PaymentTermID == request.PaymentTermID);

            if (_PaymentTerm is null)
                return Results.NotFound("Payment Term could not be found.");

            context.PaymentTerms.Remove(_PaymentTerm);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }

}
