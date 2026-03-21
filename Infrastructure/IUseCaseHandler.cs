namespace MacsBusinessManagementAPI.Infrastructure
{

    public interface IUseCaseHandler<T> where T : class
    {
        public Task<IResult> HandleAsync(T Request, CancellationToken cancellationToken);
    }

}
