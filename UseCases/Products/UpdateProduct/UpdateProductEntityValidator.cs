using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.EntityValidator;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductEntityValidator(EntityValidator existenceChecker) : IUseCaseEntityValidator<UpdateProductRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return (false, $"Product {request.ProductID} could not be found.");

        return (true, string.Empty);
    }
}
