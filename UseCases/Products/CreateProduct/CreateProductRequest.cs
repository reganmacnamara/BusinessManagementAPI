namespace BusinessManagementAPI.UseCases.Products.CreateProduct;

public class CreateProductRequest
{
    public required string ProductName { get; set; }

    public required string ProductCode { get; set; }

    public string ProductDescription { get; set; } = string.Empty;

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    public long QuantityOnHand { get; set; }
}
