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
                .SingleAsync(pt => pt.PaymentTermID == request.PaymentTermID, cancellationToken);

            var _Response = new GetPaymentTermResponse()
            {
                PaymentTerm = _PaymentTerm,
            };

            return Results.Ok(_PaymentTerm);
        }
    }

}
