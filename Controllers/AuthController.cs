using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Auth.Login;
using MacsBusinessManagementAPI.UseCases.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    [EnableRateLimiting("Unauthenticated")]

    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IResult> Login([FromBody] LoginAccountRequest request,
            [FromServices] PipelineMediator<LoginAccountRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);

        [HttpPost("Register")]
        public async Task<IResult> Register([FromBody] RegisterAccountRequest request,
            [FromServices] PipelineMediator<RegisterAccountRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);
    }
}
