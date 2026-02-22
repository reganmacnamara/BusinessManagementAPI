using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Receipts.GetReceipts;

public class GetReceiptsResponse
{
    public List<Receipt> Receipts { get; set; } = [];
}
