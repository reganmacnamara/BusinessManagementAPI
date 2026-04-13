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
        public async Task<IResult> GetCompany(
            [FromServices] PipelineMediator<GetCompanyRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

        [HttpPost]
        public async Task<IResult> UpdateCompany([FromBody] UpdateCompanyRequest request,
            [FromServices] PipelineMediator<UpdateCompanyRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);

        [HttpPost("Register")]
        public async Task<IResult> RegisterCompany([FromBody] UpdateCompanyRequest request,
            [FromServices] PipelineMediator<UpdateCompanyRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);
    }

}
