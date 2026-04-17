using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Clients.DeleteClient
{

    public class DeleteClientEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteClientRequest>
    {
        public async Task<EntityValidationResult> ValidateAsync(DeleteClientRequest request, CancellationToken cancellationToken)
        {
            if (!existenceChecker.ValidateEntityExists<Client>(request.ClientID))
                return EntityValidationResult.Failure(nameof(Client), request.ClientID);

            return EntityValidationResult.Success();
        }
    }

}
