using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.UpdateCompanyDetails
{

    public class UpdateCompanyDetailsEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpdateCompanyDetailsRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return (false, "Company could not be found.");

            return (true, string.Empty);
        }
    }

}
