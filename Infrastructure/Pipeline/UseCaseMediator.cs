namespace MacsBusinessManagementAPI.Infrastructure.Pipeline
{

    public class UseCaseMediator<TRequest>(IHttpContextAccessor contextAccessor) where TRequest : IUseCaseRequest
    {
        public async Task<IResult> InvokeUseCaseAsync(TRequest request, CancellationToken cancellationToken)
        {
            var _Services = contextAccessor.HttpContext!.RequestServices;

            var _EntityValidator = _Services.GetService<IUseCaseEntityValidator<TRequest>>();
            var _Handler = _Services.GetRequiredService<IUseCaseHandler<TRequest>>();

            // Entity Validation
            if (_EntityValidator is not null)
            {
                var _Result = await _EntityValidator.ValidateAsync(request, cancellationToken);

                if (!_Result.IsSuccess)
                    return Results.NotFound(_Result.ValidationMessage);
            }

            return await _Handler.HandleAsync(request, cancellationToken);
        }
    }

}
