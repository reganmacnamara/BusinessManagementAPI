namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public interface IUseCaseEntityValidator<TRequest> where TRequest : IUseCaseRequest
    {
        public Task<(bool result, string errorMessage)> ValidateAsync(TRequest request, CancellationToken cancellationToken);
    }

}
