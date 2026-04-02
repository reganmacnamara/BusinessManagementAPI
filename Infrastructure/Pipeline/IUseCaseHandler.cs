namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public interface IUseCaseHandler<T> where T : IUseCaseRequest
    {
        public Task<IResult> HandleAsync(T request, CancellationToken cancellationToken);
    }

}
