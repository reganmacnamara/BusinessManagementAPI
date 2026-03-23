using MacsBusinessManagementAPI.Infrastructure.Pipeline;

namespace MacsBusinessManagementAPI.UseCases.Products.UpdateProduct;

public class UpdateProductRequest : IUseCaseRequest
{
    public long ProductID { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ProductCode { get; set; } = string.Empty;

    public string ProductDescription { get; set; } = string.Empty;

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    public long QuantityOnHand { get; set; }
}
