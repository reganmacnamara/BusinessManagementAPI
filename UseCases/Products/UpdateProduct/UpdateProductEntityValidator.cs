using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateProductRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return EntityValidationResult.Failure(nameof(Product), request.ProductID);

        return EntityValidationResult.Success();
    }
}
