using System.ComponentModel.DataAnnotations;

namespace InvoiceAutomationAPI.Models;

public class Product
{
    [Key]
    public long ProductID { get; set; }

    public required string ProductName { get; set; }

    public required string ProductCode { get; set; }

    public string ProductDescription { get; set; } = string.Empty;

    public decimal UnitCost { get; set; }

    public decimal UnitPrice { get; set; }

    public long QuantityOnHand { get; set; }
}
