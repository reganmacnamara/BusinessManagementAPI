using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.GetCompanySettings
{

    public class GetCompanySettingsEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<GetCompanySettingsRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(GetCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return EntityValidationResult.Failure(nameof(Company), context.CompanyID);

            return EntityValidationResult.Success();
        }
    }

}
