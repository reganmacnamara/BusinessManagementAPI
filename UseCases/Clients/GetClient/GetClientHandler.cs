using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> GetClient(GetClientRequest request)
        {
            var _Client = m_Context.GetEntities<Client>()
                .AsNoTracking()
                .SingleOrDefault(c => c.ClientID == request.ClientId);

            if (_Client is null)
                return Results.NotFound("Client not found.");

            var _Response = m_Mapper.Map<GetClientResponse>(_Client);

            return Results.Ok(_Response);
        }
    }

}
