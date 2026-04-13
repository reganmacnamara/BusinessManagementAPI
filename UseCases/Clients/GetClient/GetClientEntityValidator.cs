using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<GetClientRequest>
    {
        public async Task<(bool result, string errorMessage)> ValidateAsync(GetClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientId))
                return (false, $"Client {request.ClientId} could not be found.");

            return (true, string.Empty);
        }
    }

}
