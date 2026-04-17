using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.UpdateClient
{

    public class UpdateClientEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateClientRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(UpdateClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientId))
                return EntityValidationResult.Failure(nameof(Client), request.ClientId);

            return EntityValidationResult.Success();
        }
    }

}
