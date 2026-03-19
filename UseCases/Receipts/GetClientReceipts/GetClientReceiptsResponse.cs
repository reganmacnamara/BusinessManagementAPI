using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetClientReceipts;

public class GetClientReceiptsResponse
{
    public List<Receipt> Receipts { get; set; } = [];
}
