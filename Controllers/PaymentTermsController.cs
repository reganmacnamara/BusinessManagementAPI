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
            [FromServices] UseCaseMediator<CreatePaymentTermRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);

        [HttpDelete("{paymentTermID}")]
        public async Task<IResult> DeletePaymentTerm([FromRoute] long paymentTermID,
            [FromServices] UseCaseMediator<DeletePaymentTermRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new() { PaymentTermID = paymentTermID }, cancellationToken);

        [HttpGet("{paymentTermID}")]
        public async Task<IResult> GetPaymentTerm([FromRoute] long paymentTermID,
            [FromServices] UseCaseMediator<GetPaymentTermRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new() { PaymentTermID = paymentTermID }, cancellationToken);

        [HttpGet]
        public async Task<IResult> GetPaymentTerms(
            [FromServices] UseCaseMediator<GetPaymentTermsRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

        [HttpPatch]
        public async Task<IResult> UpdatePaymentTerm([FromBody] UpdatePaymentTermRequest request,
            [FromServices] UseCaseMediator<UpdatePaymentTermRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);
    }

}
