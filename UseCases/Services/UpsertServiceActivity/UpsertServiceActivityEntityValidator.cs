using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.UpsertServiceActivity;

public class UpsertServiceActivityEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpsertServiceActivityRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpsertServiceActivityRequest request, CancellationToken cancellationToken)
    {
        if (request.ServiceActivityID != 0)
        {
            if (!existenceChecker.ValidateEntityExists<ServiceActivity>(request.ServiceActivityID))
                return EntityValidationResult.Failure(nameof(ServiceActivity), request.ServiceActivityID);
        }
        else
        {
            if (!existenceChecker.ValidateEntityExists<Service>(request.ServiceID))
                return EntityValidationResult.Failure(nameof(Service), request.ServiceID);
        }

        return EntityValidationResult.Success();
    }
}
