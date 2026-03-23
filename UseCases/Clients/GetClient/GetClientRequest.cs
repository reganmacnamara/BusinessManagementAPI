using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientRequest : IUseCaseRequest
    {
        public long ClientId { get; set; }
    }

}
