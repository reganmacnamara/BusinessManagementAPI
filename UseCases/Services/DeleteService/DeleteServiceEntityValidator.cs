using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.DeleteService;

public class DeleteServiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteServiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteServiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Service>(request.ServiceID))
            return EntityValidationResult.Failure(nameof(Service), request.ServiceID);

        return EntityValidationResult.Success();
    }
}
