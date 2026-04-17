using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.GetCompany
{

    public class GetCompanyEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<GetCompanyRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return EntityValidationResult.Failure(nameof(Company), context.CompanyID);

            return EntityValidationResult.Success();
        }
    }

}
