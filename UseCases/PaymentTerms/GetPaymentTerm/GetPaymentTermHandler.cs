using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerm
{

    public class GetPaymentTermHandler(SQLContext context) : IUseCaseHandler<GetPaymentTermRequest>
    {
        public async Task<IResult> HandleAsync(GetPaymentTermRequest request, CancellationToken cancellationToken)
        {
            var _PaymentTerm = await context.GetEntities<PaymentTerm>()
                .AsNoTracking()
                .SingleOrDefaultAsync(pt => pt.PaymentTermID == request.PaymentTermID, cancellationToken);

            if (_PaymentTerm is null)
                return Results.NotFound("Payment Term could not be found.");

            var _Response = new GetPaymentTermResponse()
            {
                PaymentTerm = _PaymentTerm,
            };

            return Results.Ok(_PaymentTerm);
        }
    }

}
