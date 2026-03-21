using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Extensions;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(SQLContext context) : IUseCaseHandler<UpdateClientRequest>
    {
        public async Task<IResult> HandleAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = context.GetEntities<Client>()
                .SingleOrDefault(c => c.ClientID == request.ClientId);

            if (_Client is null)
                return Results.NotFound("Client was not found.");

            _Client.UpdateFromEntity(request, ["ClientID"]);

            await context.SaveChangesAsync(cancellationToken);

            var _Response = new UpdateClientResponse()
            {
                ClientID = _Client.ClientID
            };

            return Results.Ok(_Response);
        }
    }

}
