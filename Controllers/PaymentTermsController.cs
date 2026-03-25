using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.PaymentTerms.CreatePaymentTerms;
using MacsBusinessManagementAPI.UseCases.PaymentTerms.DeletePaymentTerms;
using MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerm;
using MacsBusinessManagementAPI.UseCases.PaymentTerms.GetPaymentTerms;
using MacsBusinessManagementAPI.UseCases.PaymentTerms.UpdatePaymentTerms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [EnableRateLimiting("Authenticated")]
    public class PaymentTermsController : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreatePaymentTerm([FromBody] CreatePaymentTermRequest request,
            [FromServices] IUseCaseHandler<CreatePaymentTermRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }

        [HttpDelete("{paymentTermID}")]
        public async Task<IResult> DeletePaymentTerm([FromRoute] long paymentTermID,
            [FromServices] IUseCaseHandler<DeletePaymentTermRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(new() { PaymentTermID = paymentTermID }, cancellationToken);

            return _Response;
        }

        [HttpGet("{paymentTermID}")]
        public async Task<IResult> GetPaymentTerm([FromRoute] long paymentTermID,
            [FromServices] IUseCaseHandler<GetPaymentTermRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(new() { PaymentTermID = paymentTermID }, cancellationToken);

            return _Result;
        }

        [HttpGet]
        public async Task<IResult> GetPaymentTerms([FromServices] IUseCaseHandler<GetPaymentTermsRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(new(), cancellationToken);

            return _Result;
        }

        [HttpPatch]
        public async Task<IResult> UpdatePaymentTerm([FromBody] UpdatePaymentTermRequest request,
            [FromServices] IUseCaseHandler<UpdatePaymentTermRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(request, cancellationToken);

            return _Result;
        }
    }

}
