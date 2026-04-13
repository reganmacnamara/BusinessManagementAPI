using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<UpdateClientRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientId))
                return (false, $"Client {request.ClientId} could not be found.");

            return (true, string.Empty);
        }
    }

}
