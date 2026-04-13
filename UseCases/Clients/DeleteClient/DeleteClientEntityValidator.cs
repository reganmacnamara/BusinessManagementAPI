using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<DeleteClientRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
                return (false, $"Client {request.ClientID} could not be found.");

            return (true, string.Empty);
        }
    }

}
