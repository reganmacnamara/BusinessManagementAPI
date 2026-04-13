using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany
{

    public class RegisterCompanyEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpdateCompanyRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Account>(context.AccountID))
                return (false, "Account could not be found.");

            return (true, string.Empty);
        }
    }

}
