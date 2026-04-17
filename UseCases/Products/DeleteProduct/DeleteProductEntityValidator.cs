using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<DeleteProductRequest>
{
    public async Task<EntityValidationResult> ValidateAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return EntityValidationResult.Failure(nameof(Product), request.ProductID);

        return EntityValidationResult.Success();
    }
}
