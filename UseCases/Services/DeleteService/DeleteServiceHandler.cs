using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteService;

public class DeleteServiceHandler(SQLContext context) : IUseCaseHandler<DeleteServiceRequest>
{
    public async Task<IResult> HandleAsync(DeleteServiceRequest request, CancellationToken cancellationToken)
    {
        var _Service = await context.GetEntities<Service>()
            .SingleAsync(s => s.ServiceID == request.ServiceID, cancellationToken);

        var _Activities = await context.GetEntities<ServiceActivity>()
            .Where(sa => sa.ServiceID == request.ServiceID)
            .ToListAsync(cancellationToken);

        context.RemoveRange(_Activities);
        context.Remove(_Service);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}
