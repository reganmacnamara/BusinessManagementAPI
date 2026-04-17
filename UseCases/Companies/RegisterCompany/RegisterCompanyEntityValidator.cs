using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.RegisterCompany
{

    public class RegisterCompanyEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpdateCompanyRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(UpdateCompanyRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Account>(context.AccountID))
                return EntityValidationResult.Failure(nameof(Account), context.AccountID);

            return EntityValidationResult.Success();
        }
    }

}
