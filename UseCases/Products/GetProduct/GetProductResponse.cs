using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.UseCases.Products.GetProduct;

public class GetProductResponse
{
    public Product Product { get; set; } = default!;
}
