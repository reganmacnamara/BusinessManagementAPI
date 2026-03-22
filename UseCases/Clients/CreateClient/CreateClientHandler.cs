using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure;

namespace MacsBusinessManagementAPI.UseCases.Clients.CreateClient
{

    public class CreateClientHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateClientRequest>
    {
        public async Task<IResult> HandleAsync(CreateClientRequest request, CancellationToken cancellationToken)
        {
            var _Client = mapper.Map<Client>(request);

            context.Clients.Add(_Client);

            _ = await context.SaveChangesAsync(cancellationToken);

            var _Response = mapper.Map<CreateClientResponse>(_Client);

            return Results.Created(string.Empty, _Response);
        }
    }

}
