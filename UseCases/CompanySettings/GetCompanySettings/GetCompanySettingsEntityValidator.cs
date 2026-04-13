using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.GetCompanySettings
{

    public class GetCompanySettingsEntityValidator(ExistenceChecker existenceChecker, SQLContext context) : IUseCaseEntityValidator<GetCompanySettingsRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(GetCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return (false, "Company could not be found.");

            return (true, string.Empty);
        }
    }

}
