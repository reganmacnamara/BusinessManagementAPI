using InvoiceAutomationAPI.Models;

namespace InvoiceAutomationAPI.UseCases.Products.GetProduct;

public class GetProductResponse
{
    public Product Product { get; set; } = default!;
}
