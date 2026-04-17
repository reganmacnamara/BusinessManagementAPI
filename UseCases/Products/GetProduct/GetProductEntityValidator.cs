using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<GetProductRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return EntityValidationResult.Failure(nameof(Product), request.ProductID);

        return EntityValidationResult.Success();
    }
}
