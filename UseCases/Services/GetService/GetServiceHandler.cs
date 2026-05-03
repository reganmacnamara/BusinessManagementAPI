using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.GetService;

public class GetServiceHandler(SQLContext context) : IUseCaseHandler<GetServiceRequest>
{
    public async Task<IResult> HandleAsync(GetServiceRequest request, CancellationToken cancellationToken)
    {
        var _Service = await context.GetEntities<Service>()
            .AsNoTracking()
            .Include(s => s.ServiceActivities.OrderBy(sa => sa.SortOrder))
            .SingleAsync(s => s.ServiceID == request.ServiceID, cancellationToken);

        var _Response = new GetServiceResponse()
        {
            Service = _Service
        };

        return Results.Ok(_Response);
    }
}
