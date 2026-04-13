using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Companies.GetCompany
{

    public class GetCompanyHandler(SQLContext context) : IUseCaseHandler<GetCompanyRequest>
    {
        public async Task<IResult> HandleAsync(GetCompanyRequest request, CancellationToken cancellationToken)
        {
            var _Company = await context.GetEntities<Company>()
                .SingleAsync(c => c.CompanyID == context.CompanyID, cancellationToken);

            var _Response = new GetCompanyResponse()
            {
                Company = _Company
            };

            return Results.Ok(_Response);
        }
    }

}
