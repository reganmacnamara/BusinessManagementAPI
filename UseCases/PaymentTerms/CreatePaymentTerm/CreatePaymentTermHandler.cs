using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.PaymentTerms.CreatePaymentTerms
{

    public class CreatePaymentTermHandler(SQLContext context) : IUseCaseHandler<CreatePaymentTermRequest>
    {
        public async Task<IResult> HandleAsync(CreatePaymentTermRequest request, CancellationToken cancellationToken)
        {
            var _PaymentTermNameIsUsed = await context.GetEntities<PaymentTerm>()
                .AnyAsync(pt => pt.PaymentTermName == request.PaymentTermName, cancellationToken);

            if (_PaymentTermNameIsUsed)
                return Results.Conflict("Payment Term name already in use.");

            var _PaymentTerm = new PaymentTerm()
            {
                PaymentTermName = request.PaymentTermName,
                Unit = request.Unit,
                Value = request.Value,
                IsEndOf = request.IsEndOf,
                IsStartingNext = request.IsStartingNext
            };

            context.PaymentTerms.Add(_PaymentTerm);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Response = new CreatePaymentTermResponse()
            {
                PaymentTermID = _PaymentTerm.PaymentTermID
            };

            return Results.Created("", _Response);
        }
    }

}
