using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteService;

public class DeleteServiceRequest : IUseCaseRequest
{
    public long ServiceID { get; set; }
}
