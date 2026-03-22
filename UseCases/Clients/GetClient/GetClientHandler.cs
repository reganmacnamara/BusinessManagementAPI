using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<GetClientRequest>
    {
        public async Task<IResult> HandleAsync(GetClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = await context.GetEntities<Client>()
                .AsNoTracking()
                .SingleOrDefaultAsync(c => c.ClientID == request.ClientId, cancellationToken);

            if (_Client is null)
                return Results.NotFound("Client not found.");

            var _Response = mapper.Map<GetClientResponse>(_Client);

            return Results.Ok(_Response);
        }
    }

}
