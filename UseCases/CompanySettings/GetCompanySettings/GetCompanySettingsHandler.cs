using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.GetCompanySettings
{

    public class GetCompanySettingsHandler(SQLContext context) : IUseCaseHandler<GetCompanySettingsRequest>
    {
        public async Task<IResult> HandleAsync(GetCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            var _Company = await context.GetEntities<Company>()
                .Include(c => c.CompanySettings)
                .SingleAsync(c => c.CompanyID == context.CompanyID, cancellationToken);

            var _Response = new GetCompanySettingsResponse()
            {
                CompanySettings = _Company.CompanySettings is not null
                    ? _Company.CompanySettings
                    : new()
            };

            return Results.Ok(_Response);
        }
    }

}
