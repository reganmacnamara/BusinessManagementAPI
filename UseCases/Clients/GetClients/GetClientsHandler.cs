using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<GetClientsRequest>
    {
        public async Task<IResult> HandleAsync(GetClientsRequest request, CancellationToken cancellationToken)
        {
            var _Clients = await context.GetEntities<Client>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var _Response = _Clients.Count != 0
                ? mapper.Map<GetClientsResponse>(_Clients)
                : new GetClientsResponse();

            return Results.Ok(_Response);
        }
    }

}
