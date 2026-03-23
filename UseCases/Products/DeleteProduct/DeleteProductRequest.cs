using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.DeleteProduct;

public class DeleteProductRequest : IUseCaseRequest
{
    public long ProductID { get; set; }
}
