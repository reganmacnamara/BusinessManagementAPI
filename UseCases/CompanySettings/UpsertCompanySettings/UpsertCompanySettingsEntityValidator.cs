using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.UpsertCompanySettings
{

    public class UpsertCompanySettingsEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpsertCompanySettingsRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(UpsertCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return EntityValidationResult.Failure(nameof(Company), context.CompanyID);

            return EntityValidationResult.Success();
        }
    }

}
