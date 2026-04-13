using MacsBusinessManagementAPI.Entities;
using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductEntityValidator(ExistenceChecker existenceChecker) : IEntityValidator<GetProductRequest>
{
    public async Task<(bool result, string errorMessage)> ValidateAsync(GetProductRequest request, CancellationToken cancellationToken)
    {
        if (!existenceChecker.ValidateEntityExists<Product>(request.ProductID))
            return (false, $"Product {request.ProductID} could not be found.");

        return (true, string.Empty);
    }
}
