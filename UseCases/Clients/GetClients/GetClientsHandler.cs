using AutoMapper;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Clients.GetClients
{

    public class GetClientsHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetClientsResponse> GetClients()
        {
            var _Clients = await m_Context.Clients.ToListAsync();

            var _Response = _Clients.Count != 0
                ? m_Mapper.Map<GetClientsResponse>(_Clients)
                : new GetClientsResponse();

            return _Response;
        }
    }

}
