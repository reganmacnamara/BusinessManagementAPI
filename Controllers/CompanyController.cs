using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.Companies.GetCompany;
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
        [HttpGet]
        public async Task<IResult> GetCompany([FromBody] GetCompanyRequest request,
            [FromServices] IUseCaseHandler<GetCompanyRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }

        [HttpPost]
        public async Task<IResult> UpdateCompany([FromBody] UpdateCompanyRequest request,
            [FromServices] IUseCaseHandler<UpdateCompanyRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }

        [HttpPost("Register")]
        public async Task<IResult> RegisterCompany([FromBody] UpdateCompanyRequest request,
            [FromServices] IUseCaseHandler<UpdateCompanyRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }
    }

}
