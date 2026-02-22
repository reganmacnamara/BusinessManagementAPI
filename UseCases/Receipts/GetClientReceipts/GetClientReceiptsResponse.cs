using BusinessManagementAPI.Entities;

namespace BusinessManagementAPI.UseCases.Receipts.GetClientReceipts;

public class GetClientReceiptsResponse
{
    public List<Receipt> Receipts { get; set; } = [];
}
