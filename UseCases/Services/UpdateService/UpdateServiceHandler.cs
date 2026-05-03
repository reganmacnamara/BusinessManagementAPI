using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.UpdateService;

public class UpdateServiceHandler(SQLContext context) : IUseCaseHandler<UpdateServiceRequest>
{
    public async Task<IResult> HandleAsync(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        var _Service = await context.GetEntities<Service>()
            .SingleAsync(s => s.ServiceID == request.ServiceID, cancellationToken);

        _Service.UpdateFromEntity(request, [nameof(Service.ServiceID)]);

        _ = await context.SaveChangesAsync(cancellationToken);

        var _Response = new UpdateServiceResponse()
        {
            ServiceID = _Service.ServiceID
        };

        return Results.Ok(_Response);
    }
}
