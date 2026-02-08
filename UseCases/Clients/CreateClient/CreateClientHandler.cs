using AutoMapper;
using BusinessManagementAPI.Data;
using BusinessManagementAPI.Entities;
using BusinessManagementAPI.UseCases.Base;

namespace BusinessManagementAPI.UseCases.Clients.CreateClient
{

    public class CreateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context)
    {
        public async Task<IResult> CreateClient(CreateClientRequest request)
        {
            var _Client = m_Mapper.Map<Client>(request);

            m_Context.Clients.Add(_Client);

            await m_Context.SaveChangesAsync();

            var _Response = m_Mapper.Map<CreateClientResponse>(_Client);

            return Results.Created(string.Empty, _Response);
        }
    }

}
