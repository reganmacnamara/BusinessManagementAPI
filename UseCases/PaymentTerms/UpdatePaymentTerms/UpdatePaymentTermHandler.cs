using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.UpdatePaymentTerms
{

    public class UpdatePaymentTermHandler(SQLContext context) : IUseCaseHandler<UpdatePaymentTermRequest>
    {
        public async Task<IResult> HandleAsync(UpdatePaymentTermRequest request, CancellationToken cancellationToken)
        {
            var _PaymentTermNameIsUsed = await context.GetEntities<PaymentTerm>()
                .AnyAsync(pt => pt.PaymentTermName == request.PaymentTermName && pt.PaymentTermID != request.PaymentTermID, cancellationToken);

            if (_PaymentTermNameIsUsed)
                return Results.Conflict("Payment Term name already in use.");

            var _PaymentTerm = await context.GetEntities<PaymentTerm>()
                .SingleOrDefaultAsync(pt => pt.PaymentTermID == request.PaymentTermID, cancellationToken);

            if (_PaymentTerm is null)
                return Results.NotFound("Payment Term could not be found.");

            _PaymentTerm.UpdateFromEntity(request, [nameof(PaymentTerm.PaymentTermID)]);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }
}

