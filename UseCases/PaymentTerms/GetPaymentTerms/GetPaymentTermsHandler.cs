using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerms
{

    public class GetPaymentTermsHandler(SQLContext context) : IUseCaseHandler<GetPaymentTermsRequest>
    {
        public async Task<IResult> HandleAsync(GetPaymentTermsRequest request, CancellationToken cancellationToken)
        {
            var _PaymentTerms = await context.GetEntities<PaymentTerm>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var _Response = new GetPaymentTermsResponse()
            {
                PaymentTerms = _PaymentTerms
            };

            return Results.Ok(_Response);
        }
    }

}
