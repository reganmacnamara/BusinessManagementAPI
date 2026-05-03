using AutoMapper;
using MacsBusinessManagementAPI.Data;
using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.CreateService;

public class CreateServiceHandler(IMapper mapper, SQLContext context) : IUseCaseHandler<CreateServiceRequest>
{
    public async Task<IResult> HandleAsync(CreateServiceRequest request, CancellationToken cancellationToken)
    {
        var _Service = mapper.Map<Service>(request);

        context.Services.Add(_Service);

        _ = await context.SaveChangesAsync(cancellationToken);

        return Results.Created(string.Empty, new CreateServiceResponse() { ServiceID = _Service.ServiceID });
    }
}
