using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteServiceActivity;

public class DeleteServiceActivityRequest : IUseCaseRequest
{
    public long ServiceActivityID { get; set; }
}
