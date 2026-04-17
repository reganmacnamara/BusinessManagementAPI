using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.GetClient
{

    public class GetClientEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetClientRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(GetClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientId))
                return EntityValidationResult.Failure(nameof(Client), request.ClientId);

            return EntityValidationResult.Success();
        }
    }

}
