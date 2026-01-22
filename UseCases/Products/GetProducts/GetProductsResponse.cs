using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Products.GetProducts;

public class GetProductsResponse
{
    public List<Product> Products { get; set; } = [];
}
