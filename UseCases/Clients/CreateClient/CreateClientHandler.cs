using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;
using MacsBusinessManagementAPI.UseCases.Base;

namespace MacsBusinessManagementAPI.UseCases.Clients.CreateClient
{

    public class CreateClientHandler(IMapper mapper, SQLContext context) : BaseHandler(mapper, context), IUseCaseHandler<CreateClientRequest>
    {
        public async Task<IResult> HandleAsync(CreateClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = m_Mapper.Map<Client>(request);

            m_Context.Clients.Add(_Client);

            await m_Context.SaveChangesAsync(cancellationToken);

            var _Response = m_Mapper.Map<CreateClientResponse>(_Client);

            return Results.Created(string.Empty, _Response);
        }
    }

}
