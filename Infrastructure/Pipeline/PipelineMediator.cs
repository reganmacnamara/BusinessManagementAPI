namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public class PipelineMediator<TRequest>(IHttpContextAccessor contextAccessor) where TRequest : IUseCaseRequest
    {
        public async Task<IResult> InvokeUseCaseAsync(TRequest request, CancellationToken cancellationToken)
        {
            var _Services = contextAccessor.HttpContext!.RequestServices;

            var _EntityValidator = _Services.GetRequiredService<IEntityValidator<TRequest>>();
            var _Handler = _Services.GetRequiredService<IUseCaseHandler<TRequest>>();

            // Entity Validation
            if (_EntityValidator is not null)
            {
                (bool _Result, string _ErrorMessage) = await _EntityValidator.ValidateAsync(request, cancellationToken);

                if (!_Result)
                    return Results.NotFound(_ErrorMessage);
            }

            return await _Handler.HandleAsync(request, cancellationToken);
        }
    }

}
