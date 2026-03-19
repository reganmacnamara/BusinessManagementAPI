using MacsBusinessManagementAPI.Entities;

namespace MacsBusinessManagementAPI.UseCases.Receipts.GetReceipt;

public class GetReceiptResponse
{
    public Receipt Receipt { get; set; } = default!;

    public List<ReceiptItem> ReceiptItems { get; set; } = [];
}
