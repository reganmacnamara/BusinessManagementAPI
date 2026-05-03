using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.UpdateService;

public class UpdateServiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateServiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpdateServiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Service>(request.ServiceID))
            return EntityValidationResult.Failure(nameof(Service), request.ServiceID);

        return EntityValidationResult.Success();
    }
}
