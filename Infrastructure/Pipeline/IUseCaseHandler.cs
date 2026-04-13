namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public interface IUseCaseHandler<TRequest> where TRequest : IUseCaseRequest
    {
        public Task<IResult> HandleAsync(TRequest request, CancellationToken cancellationToken);
    }

}
