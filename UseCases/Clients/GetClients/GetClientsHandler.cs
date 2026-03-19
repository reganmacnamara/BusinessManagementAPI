using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> GetClients()
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
