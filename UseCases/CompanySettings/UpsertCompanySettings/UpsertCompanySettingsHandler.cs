using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.UpsertCompanySettings
{

    public class UpsertCompanySettingsHandler(SQLContext context) : IUseCaseHandler<UpsertCompanySettingsRequest>
    {
        public async Task<IResult> HandleAsync(UpsertCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            var _Company = await context.GetEntities<Company>()
                .Include(c => c.CompanySettings)
                .SingleAsync(c => c.CompanyID == context.CompanyID, cancellationToken);

            //Update
            if (_Company.CompanySettingsID != 0 && _Company.CompanySettings is not null)
            {
                _Company.CompanySettings.UpdateFromEntity(request, [nameof(Entities.CompanySettings.CompanySettingsID)]);

                _ = context.SaveChangesAsync(cancellationToken);
            }
            //Create
            else
            {
                var _CompanySettings = new Entities.CompanySettings();

                _CompanySettings.UpdateFromEntity(request, [nameof(Entities.CompanySettings.CompanySettingsID)]);

                _Company.CompanySettings = _CompanySettings;

                _ = context.SaveChangesAsync(cancellationToken);
            }

            return Results.NoContent();
        }
    }

}
