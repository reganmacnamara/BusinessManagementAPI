using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using MacsBusinessManagementAPI.UseCases.CompanySettings.GetCompanySettings;
using MacsBusinessManagementAPI.UseCases.CompanySettings.UpsertCompanySettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MacsBusinessManagementAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [EnableRateLimiting("Authenticated")]
    public class CompanySettingsController
    {
        [HttpGet]
        public async Task<IResult> GetCompanySettings(
            [FromServices] UseCaseMediator<GetCompanySettingsRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(new(), cancellationToken);

        [HttpPost]
        public async Task<IResult> UpsertCompanySettings([FromBody] UpsertCompanySettingsRequest request,
            [FromServices] UseCaseMediator<UpsertCompanySettingsRequest> mediator,
            CancellationToken cancellationToken)
            => await mediator.InvokeUseCaseAsync(request, cancellationToken);
    }

}
