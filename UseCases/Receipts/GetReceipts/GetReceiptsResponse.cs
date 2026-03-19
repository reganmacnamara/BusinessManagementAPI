using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsResponse
{
    public List<Receipt> Receipts { get; set; } = [];
}
