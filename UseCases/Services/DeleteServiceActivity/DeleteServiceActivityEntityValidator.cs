using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteServiceActivity;

public class DeleteServiceActivityEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteServiceActivityRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteServiceActivityRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<ServiceActivity>(request.ServiceActivityID))
            return EntityValidationResult.Failure(nameof(ServiceActivity), request.ServiceActivityID);

        return EntityValidationResult.Success();
    }
}
