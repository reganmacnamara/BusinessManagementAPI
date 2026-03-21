using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<UpdateClientRequest>
    {
        public async Task<IResult> HandleAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = m_Context.GetEntities<Client>()
                .SingleOrDefault(c => c.ClientID == request.ClientId);

            if (_Client is null)
                return Results.NotFound("Client was not found.");

            _Client = UpdateEntityFromRequest(_Client, request, ["ClientID"]);

            await m_Context.SaveChangesAsync(cancellationToken);

            var _Response = new UpdateClientResponse()
            {
                ClientID = _Client.ClientID
            };

            return Results.Ok(_Response);
        }
    }

}
