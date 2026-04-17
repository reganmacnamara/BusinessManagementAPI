using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Companies.UpdateCompanyDetails
{

    public class UpdateCompanyDetailsEntityValidator(EntityValidator existenceChecker, SQLContext context) : IUseCaseEntityValidator<UpdateCompanyDetailsRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(UpdateCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Company>(context.CompanyID))
                return EntityValidationResult.Failure(nameof(Company), context.CompanyID);

            return EntityValidationResult.Success();
        }
    }

}
