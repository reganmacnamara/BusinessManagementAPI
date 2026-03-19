using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsResponse
{
    public List<Product> Products { get; set; } = [];
}
