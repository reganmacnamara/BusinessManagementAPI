using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.CreateClient
{

    public class CreateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<CreateClientResponse> CreateClient(CreateClientRequest request)
        {
            var _Client = m_Mapper.Map<Client>(request);

            m_Context.Clients.Add(_Client);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateClientResponse>(_Client);

            return _Response;
        }
    }

}
