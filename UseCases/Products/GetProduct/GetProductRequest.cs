using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductRequest : IUseCaseRequest
{
    public long ProductID { get; set; }
}
