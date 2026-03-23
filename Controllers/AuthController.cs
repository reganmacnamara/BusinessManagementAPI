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
        [HttpPost("Register")]
        public async Task<IResult> Register([FromBody] RegisterAccountRequest request,
            [FromServices] IUseCaseHandler<RegisterAccountRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(request, cancellationToken);

            return _Result;
        }

        [HttpPost("Login")]
        public async Task<IResult> Login([FromBody] LoginAccountRequest request,
            [FromServices] IUseCaseHandler<LoginAccountRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Result = await handler.HandleAsync(request, cancellationToken);

            return _Result;
        }
    }
}
