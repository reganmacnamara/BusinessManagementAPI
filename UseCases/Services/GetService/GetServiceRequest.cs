using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.GetService;

public class GetServiceRequest : IUseCaseRequest
{
    public long ServiceID { get; set; }
}
