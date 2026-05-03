using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteServiceActivity;

public class DeleteServiceActivityHandler(SQLContext context) : IUseCaseHandler<DeleteServiceActivityRequest>
{
    public async Task<IResult> HandleAsync(DeleteServiceActivityRequest request, CancellationToken cancellationToken)
    {
        var _Activity = await context.GetEntities<ServiceActivity>()
            .SingleAsync(sa => sa.ServiceActivityID == request.ServiceActivityID, cancellationToken);

        context.ServiceActivities.Remove(_Activity);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
