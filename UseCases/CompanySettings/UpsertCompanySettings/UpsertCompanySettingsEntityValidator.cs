using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.CompanySettings.UpsertCompanySettings
{

    public class UpsertCompanySettingsEntityValidator(ExistenceChecker existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpsertCompanySettingsRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(UpsertCompanySettingsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return (false, "Company could not be found.");

            return (true, string.Empty);
        }
    }

}
