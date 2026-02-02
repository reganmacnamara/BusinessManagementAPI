using AutoMapper;
using BusinessManagementAPI.UseCases.Base;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientHandler(IMapper mapper) : BaseHandler(mapper)
    {
        public async Task<GetClientResponse> GetClient(GetClientRequest request)
        {
            var _Client = await m_Context.Clients.Where(client => client.ClientID == request.ClientId).SingleAsync();

            var _Response = m_Mapper.Map<GetClientResponse>(_Client);

            return _Response;
        }
    }

}
