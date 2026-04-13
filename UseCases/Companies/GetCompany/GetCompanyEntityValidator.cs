using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.GetCompany
{

    public class GetCompanyEntityValidator(ExistenceChecker existenceChecker, SQLContext context) : IEntityValidator<GetCompanyRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return (false, "Company could not be found.");

            return (true, string.Empty);
        }
    }

}
