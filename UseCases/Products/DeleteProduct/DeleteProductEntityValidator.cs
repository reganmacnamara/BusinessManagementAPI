using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.ExistenceChecker;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<DeleteProductRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return (false, $"Product {request.ProductID} could not be found.");

        return (true, string.Empty);
    }
}
