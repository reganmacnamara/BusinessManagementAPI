using MacsBusinessManagementAPI.Infrastructure.EntityValidator;

namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public interface IUseCaseEntityValidator<TRequest> where TRequest : IUseCaseRequest
    {
        public Task<EntityValidationResult> ValidateAsync(TRequest request, CancellationToken cancellationToken);
    }

}
