using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Products.GetProducts;

public class GetProductsResponse
{
    public List<Product> Products { get; set; } = [];
}
