using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [EnableRateLimiting("Authenticated")]
    public class CompanyController : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IResult> RegisterCompany([FromBody] RegisterCompanyRequest request,
            [FromServices] IUseCaseHandler<RegisterCompanyRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }
    }

}
