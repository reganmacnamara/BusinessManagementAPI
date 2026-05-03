using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.GetServices;

public class GetServicesHandler(SQLContext context) : IUseCaseHandler<GetServicesRequest>
{
    public async Task<IResult> HandleAsync(GetServicesRequest request, CancellationToken cancellationToken)
    {
        var _Services = await context.GetEntities<Service>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var _Response = new GetServicesResponse
        {
            Services = _Services
        };

        return Results.Ok(_Response);
    }
}
