using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<GetClientsRequest>
    {
        public async Task<IResult> HandleAsync(GetClientsRequest request, CancellationToken cancellationToken)
        {
            var _Clients = m_Context.GetEntities<Client>()
                .AsNoTracking()
                .ToList();

            var _Response = _Clients.Count != 0
                ? m_Mapper.Map<GetClientsResponse>(_Clients)
                : new GetClientsResponse();

            return Results.Ok(_Response);
        }
    }

}
