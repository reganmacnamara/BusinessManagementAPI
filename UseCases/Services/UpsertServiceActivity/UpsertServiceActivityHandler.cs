using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Services.UpsertServiceActivity;

public class UpsertServiceActivityHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<UpsertServiceActivityRequest>
{
    public async Task<IResult> HandleAsync(UpsertServiceActivityRequest request, CancellationToken cancellationToken)
    {
        if (request.ServiceActivityID != 0)
        {
            var _Activity = await context.GetEntities<ServiceActivity>()
                .SingleAsync(sa => sa.ServiceActivityID == request.ServiceActivityID, cancellationToken);

            _Activity.UpdateFromEntity(request, [nameof(ServiceActivity.ServiceActivityID)]);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Response = new UpsertServiceActivityResponse()
            {
                ServiceActivityID = _Activity.ServiceActivityID
            };

            return Results.Ok(_Response);
        }
        else
        {
            var _Service = await context.GetEntities<Service>()
                .SingleAsync(s => s.ServiceID == request.ServiceID, cancellationToken);

            var _Activity = mapper.Map<ServiceActivity>(request);

            _Activity.Service = _Service;

            context.ServiceActivities.Add(_Activity);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Response = new UpsertServiceActivityResponse()
            {
                ServiceActivityID = _Activity.ServiceActivityID
            };

            return Results.Created(string.Empty, _Response);
        }
    }
}
