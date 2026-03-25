using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(SQLContext context) : IUseCaseHandler<UpdateClientRequest>
    {
        public async Task<IResult> HandleAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = await context.GetEntities<Client>()
                .SingleOrDefaultAsync(c => c.ClientID == request.ClientId, cancellationToken);

            if (_Client is null)
                return Results.NotFound("Client was not found.");

            _Client.UpdateFromEntity(request, [nameof(Client.ClientID)]);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Response = new UpdateClientResponse()
            {
                ClientID = _Client.ClientID
            };

            return Results.Ok(_Response);
        }
    }

}
