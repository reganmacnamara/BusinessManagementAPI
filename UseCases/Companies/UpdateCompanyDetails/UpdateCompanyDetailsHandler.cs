using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.ABNValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Companies.UpdateCompanyDetails
{

    public class UpdateCompanyDetailsHandler(SQLContext context) : IUseCaseHandler<UpdateCompanyDetailsRequest>
    {
        public async Task<IResult> HandleAsync(UpdateCompanyDetailsRequest request, CancellationToken cancellationToken)
        {
            if (!ABNValidator.IsValidABN(request.CompanyABN))
                return Results.BadRequest("ABN is not valid.");

            var _Company = await context.GetEntities<Company>()
                .SingleAsync(c => c.CompanyID == context.CompanyID, cancellationToken);

            _Company.UpdateFromEntity(request, [nameof(Company.CompanyID)]);

            _ = await context.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        }
    }

}
