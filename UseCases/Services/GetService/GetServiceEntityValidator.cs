using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Services.GetService;

public class GetServiceEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetServiceRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetServiceRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Service>(request.ServiceID))
            return EntityValidationResult.Failure(nameof(Service), request.ServiceID);

        return EntityValidationResult.Success();
    }
}
