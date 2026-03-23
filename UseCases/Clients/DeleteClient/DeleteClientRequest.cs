using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientRequest : IUseCaseRequest
    {
        public long ClientID { get; set; }
    }

}
