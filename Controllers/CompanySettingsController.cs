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
        public async Task<IResult> GetCompanySettings([FromServices] IUseCaseHandler<GetCompanySettingsRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(new(), cancellationToken);

            return _Response;
        }

        [HttpPost]
        public async Task<IResult> UpsertCompanySettings([FromBody] UpsertCompanySettingsRequest request,
            [FromServices] IUseCaseHandler<UpsertCompanySettingsRequest> handler,
            CancellationToken cancellationToken)
        {
            var _Response = await handler.HandleAsync(request, cancellationToken);

            return _Response;
        }
    }

}
