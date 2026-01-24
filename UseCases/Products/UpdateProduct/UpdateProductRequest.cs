namespace InvoiceAutomationAPI.UseCases.Products.UpdateProduct;

public class UpdateProductRequest
{
    public long ProductID { get; set; }

    public string ProductName { get; set; }

    public string ProductCode { get; set; }

    public string ProductDescription { get; set; }

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    public long QuantityOnHand { get; set; }
}
